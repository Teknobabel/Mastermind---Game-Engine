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

		foreach (Player.ActorSlot aSlot in player.hiringPool.m_hireSlots) {

			if (aSlot.m_state == Player.ActorSlot.ActorSlotState.Empty && player.hiringPool.m_availableHenchmen.Count > 0) {

				// build list of henchmen that fit Infamy criteria

//				int rank2Threshold = 20;
//				int rank3Threshold = 50;
//				int rank4Threshold = 10;
//
//				int maxRank = 1;
//
//				if (player.infamy >= rank4Threshold) {
//
//					maxRank = 4;
//				} else if (player.infamy >= rank3Threshold) {
//					maxRank = 3;
//				}
//				else if (player.infamy >= rank2Threshold) {
//					maxRank = 2;
//				}
//
//				List<Actor> validHenchmen = new List<Actor> ();
//
//				foreach (Actor a in player.hiringPool.m_availableHenchmen) {
//
//					if (a.m_rank <= maxRank) {
//
//						validHenchmen.Add (a);
//					}
//				}
//
//				if (validHenchmen.Count > 0) {
//
//					int rand = Random.Range (0, validHenchmen.Count);
//					Actor randHenchmen = (Actor)validHenchmen [rand];
//
//					Action_MakeHireable addToHiringPool = new Action_MakeHireable ();
//					addToHiringPool.m_player = player;
//					addToHiringPool.m_henchmen = randHenchmen;
//					GameController.instance.ProcessAction (addToHiringPool);
//				}

				Actor henchmen = player.hiringPool.GetHenchmenToHire (player.infamy);

				if (henchmen != null) {
					Action_MakeHireable addToHiringPool = new Action_MakeHireable ();
					addToHiringPool.m_player = player;
					addToHiringPool.m_henchmen = henchmen;
					GameController.instance.ProcessAction (addToHiringPool);
				}
			}
		}

		// evaluate any event triggers

		// check if intel should be spawned

//		GameEngine.instance.NextTurnPhase ();
		Debug.Log ("Ending End Turn Phase");

	}

//	public override IEnumerator EndTurnPhase ()
//	{
//		Debug.Log ("Ending End Turn Phase");
//		yield return null;
//	}
}
