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

				int rand = Random.Range (0, player.hiringPool.m_availableHenchmen.Count);
				Actor randHenchmen = (Actor)player.hiringPool.m_availableHenchmen [rand];

				Action_MakeHireable addToHiringPool = new Action_MakeHireable ();
				addToHiringPool.m_player = player;
				addToHiringPool.m_henchmen = randHenchmen;
				GameController.instance.ProcessAction (addToHiringPool);
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
