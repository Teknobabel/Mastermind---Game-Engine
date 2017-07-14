using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurnPhase_PlayerPhase : TurnPhase {

//	public override IEnumerator StartTurnPhase ()
//	{
//		Debug.Log ("Starting Player Phase");
//
//		// refill command pool
//
//		// pay henchmen
//
//		// get turn summary
//
//		yield return null;
//	}

	public override void DoTurnPhase ()
	{
		Debug.Log ("Starting Player Phase");

		// refill command pool

		Player player = GameEngine.instance.game.playerList [0];

		if (player.commandPool.m_currentPool < player.commandPool.m_basePool) {

			Action_RefillCommandPool refill = new Action_RefillCommandPool ();
			refill.m_playerID = 0;
			GameController.instance.ProcessAction (refill);
		}

		// pay henchmen

		List<Player.ActorSlot> henchmen = GameController.instance.GetHiredHenchmen (0);

		int upkeepCost = 0;

		foreach (Player.ActorSlot aSlot in henchmen) {

			if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {

				upkeepCost += aSlot.m_actor.m_turnCost;
			}
		}

		if (upkeepCost > 0) {

			Action_SpendCommandPoints payUpkeep = new Action_SpendCommandPoints ();
			payUpkeep.m_amount = upkeepCost;
			payUpkeep.m_playerID = 0;
			GameController.instance.ProcessAction (payUpkeep);
		}

		GameController.instance.Notify (player, GameEvent.Turn_PlayerPhaseStarted);

//		GameEngine.instance.NextTurnPhase ();
		Debug.Log ("Ending Player Phase");

	}

//	public override IEnumerator EndTurnPhase ()
//	{
//		Debug.Log ("Ending Player Phase");
//
//		// refill empty henchmen hire slots
//
//		// update infamy
//
//		yield return null;
//	}
}
