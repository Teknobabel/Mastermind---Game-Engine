using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurnPhase_EndTurn : TurnPhase {

//	public override IEnumerator StartTurnPhase ()
//	{
//		Debug.Log ("Starting End Turn Phase");
//		yield return null;
//	}

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




		// update effect durations

		foreach (KeyValuePair<int, Player> pair in GameController.instance.game.playerList) {

			pair.Value.effectPool.UpdateDuration ();

			foreach (Lair.FloorSlot fSlot in pair.Value.lair.floorSlots) {

				fSlot.m_floor.effectPool.UpdateDuration ();
			}
		}


//		player.missionsCompletedThisTurn.Clear ();

//		GameEngine.instance.NextTurnPhase ();
		Debug.Log ("Ending End Turn Phase");

	}

//	public override IEnumerator EndTurnPhase ()
//	{
//		Debug.Log ("Ending End Turn Phase");
//		yield return null;
//	}
}
