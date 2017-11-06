using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurnPhase_EndTurn : TurnPhase {

	public override void DoTurnPhase ()
	{
		Debug.Log ("Starting End Turn Phase");

		// refill empty henchmen hire slots

		Player player = GameEngine.instance.game.playerList [0];

		float newHenchmenAppearanceChance = 0.55f;
		float henchmenForHireLeaveChance = 0.3f;
		float turnsToWaitBeforeLeaveChance = 4;

		foreach (Player.ActorSlot aSlot in player.hiringPool.m_hireSlots) {

			if (aSlot.m_state == Player.ActorSlot.ActorSlotState.Empty && player.hiringPool.m_availableHenchmen.Count > 0 &&
				Random.Range(0.0f, 1.0f) <= newHenchmenAppearanceChance) {

				Actor henchmen = player.hiringPool.GetHenchmenToHire (player.infamy);

				if (henchmen != null) {
					Action_MakeHireable addToHiringPool = new Action_MakeHireable ();
					addToHiringPool.m_player = player;
					addToHiringPool.m_henchmen = henchmen;
					GameController.instance.ProcessAction (addToHiringPool);
				}
			} else if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {

				// waiting henchmen have a chance to leave after a while

				aSlot.m_turnsPresent++;

				if (aSlot.m_turnsPresent >= turnsToWaitBeforeLeaveChance && Random.Range (0.0f, 1.0f) <= henchmenForHireLeaveChance) {

					Action_RemoveHireable removeHenchmen = new Action_RemoveHireable ();
					removeHenchmen.m_playerID = 0;
					removeHenchmen.m_actorID = aSlot.m_actor.id;
					GameController.instance.ProcessAction (removeHenchmen);
				}
			}
		}

		// if there are any henchmen in the temp bank, add them to available henchmen pool

		while (player.hiringPool.m_tempBank.Count > 0) {

			Actor a = player.hiringPool.m_tempBank [0];
			player.hiringPool.m_tempBank.RemoveAt (0);
			player.hiringPool.m_availableHenchmen.Add (a);
		}

		// evaluate any event triggers




		// check if intel should be spawned

		int turnNumber = GameController.instance.game.currentTurn;
		int intelSpawnRate = GameController.instance.game.director.m_intelSpawnRate;

		if ((turnNumber % intelSpawnRate) == 0) {

			// spawn intel

			Action_PlaceIntel placeIntel = new Action_PlaceIntel ();
			placeIntel.m_playerID = 0;
			GameController.instance.ProcessAction (placeIntel);
		}


		// update effect durations

		foreach (KeyValuePair<int, Player> pair in GameController.instance.game.playerList) {

			pair.Value.effectPool.UpdateDuration ();

			foreach (Lair.FloorSlot fSlot in pair.Value.lair.floorSlots) {

				fSlot.m_floor.effectPool.UpdateDuration ();
			}
		}

		foreach (KeyValuePair<int, Site> pair in GameController.instance.game.siteList) {

			pair.Value.effectPool.UpdateDuration ();
		}

		// update affinity for employed henchmen

		List<Player.ActorSlot> hiredHenchmen = GameController.instance.GetHiredHenchmen (0);

		foreach (Player.ActorSlot aSlot in hiredHenchmen) {


			// get a list of other henchmen to potentially interact with

			List<Player.ActorSlot> validTargets = new List<Player.ActorSlot> ();

			if (aSlot.m_state == Player.ActorSlot.ActorSlotState.Active) {

				foreach (Player.ActorSlot vtSlot in hiredHenchmen) {

					if (vtSlot.m_state == Player.ActorSlot.ActorSlotState.Active && vtSlot.m_actor.id != aSlot.m_actor.id) {

						validTargets.Add (vtSlot);
					}
				}
			}

			// choose henchmen to interact with

			if (validTargets.Count > 0)
			{
				Player.ActorSlot targetSlot = validTargets[Random.Range(0, validTargets.Count)];

				float affinityIncreaseChance = 0.5f;

				// determine if interaction leads to affinity increase or decrease

				// chance of increase / decrease affected by henchmens current affinity score w target

				int affinityScore = aSlot.m_actor.GetAffinityScore (targetSlot.m_actor.id, (IAffinity)targetSlot.m_actor);

				if (affinityScore >= 40) {

					affinityIncreaseChance += 0.2f;

				} else if (affinityScore <= -40) {

					affinityIncreaseChance += 0.2f;
				}

				// chance of increase / decrease affected by target henchmens current affinity score w henchmen

				int targetsAffinityScore = targetSlot.m_actor.GetAffinityScore (aSlot.m_actor.id, (IAffinity)aSlot.m_actor);

				if (targetsAffinityScore >= 40) {

					affinityIncreaseChance += 0.2f;

				} else if (targetsAffinityScore <= -40) {

					affinityIncreaseChance += 0.2f;
				}

				if (Random.Range (0.0f, 1.0f) <= affinityIncreaseChance) {

					aSlot.m_actor.UpdateAffinity (targetSlot.m_actor.id, 10, (IAffinity) targetSlot.m_actor);

				} else {

					aSlot.m_actor.UpdateAffinity (targetSlot.m_actor.id, -10, (IAffinity) targetSlot.m_actor);
				}
			}
		}


		// do site end turn

		foreach (KeyValuePair<int, Site> pair in GameController.instance.game.siteList) {

			pair.Value.EndTurn ();
		}


		Debug.Log ("Ending End Turn Phase");

	}

	//	public override IEnumerator EndTurnPhase ()
	//	{
	//		Debug.Log ("Ending End Turn Phase");
	//		yield return null;
	//	}
}
