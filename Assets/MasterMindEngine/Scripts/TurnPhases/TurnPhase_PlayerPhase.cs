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

		foreach (Player.ActorSlot aSlot in henchmen) {

			if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {

				int cost = aSlot.m_actor.m_turnCost;

				if (cost <= player.commandPool.m_currentPool) {

					// can afford to pay henchmen

					Action_SpendCommandPoints payUpkeep = new Action_SpendCommandPoints ();
					payUpkeep.m_amount = cost;
					payUpkeep.m_playerID = 0;
					GameController.instance.ProcessAction (payUpkeep);

				} else {

					if (player.commandPool.m_currentPool > 0) {

						Action_SpendCommandPoints payUpkeep = new Action_SpendCommandPoints ();
						payUpkeep.m_amount = player.commandPool.m_currentPool;
						payUpkeep.m_playerID = 0;
						GameController.instance.ProcessAction (payUpkeep);
					}

					// check results of not paying henchmen

					Action_CantPayHenchmen cantPay = new Action_CantPayHenchmen ();
					cantPay.m_playerID = 0;
					cantPay.m_henchmen = aSlot.m_actor;
					GameController.instance.ProcessAction (cantPay);
				}
			}
		}

//		int upkeepCost = 0;
//
//		foreach (Player.ActorSlot aSlot in henchmen) {
//
//			if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {
//
//				upkeepCost += aSlot.m_actor.m_turnCost;
//			}
//		}
//
//		if (upkeepCost > 0) {
//
//			Action_SpendCommandPoints payUpkeep = new Action_SpendCommandPoints ();
//			payUpkeep.m_amount = upkeepCost;
//			payUpkeep.m_playerID = 0;
//			GameController.instance.ProcessAction (payUpkeep);
//		}

		// pay for any extra Assets over limit

		int overage = GameController.instance.GetAssetUpkeep(0);

		if (overage > 0) {

			string title = "Too Many Assets";
			string message = "You have more Assets than available space. 1 CP must be paid for each additional Asset.";
			player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Assets, false, -1);

			Action_SpendCommandPoints payOverage = new Action_SpendCommandPoints ();
			payOverage.m_amount = overage;
			payOverage.m_playerID = 0;
			GameController.instance.ProcessAction (payOverage);

		}

		// gain infamy

		Action_GainInfamy gainInfamy = new Action_GainInfamy ();
		gainInfamy.m_playerID = 0;
		gainInfamy.m_amount = 1;
		GameController.instance.ProcessAction (gainInfamy);

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
