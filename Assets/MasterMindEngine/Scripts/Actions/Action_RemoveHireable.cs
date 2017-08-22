using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_RemoveHireable : Action {

	public int m_playerID = 0;
	public int m_actorID = -1;

	public override void ExecuteAction ()
	{
		Player player = GameEngine.instance.game.playerList [m_playerID];

		foreach (Player.ActorSlot aSlot in player.hiringPool.m_hireSlots) {

			if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty && aSlot.m_actor.id == m_actorID) {

				Actor a = aSlot.m_actor;
				aSlot.RemoveHenchmen ();
//				player.hiringPool.m_availableHenchmen.Add (a);
				player.hiringPool.m_tempBank.Add(a);

				string title = a.m_actorName + " Left";
				string message = a.m_actorName + " is no longer available for hire.";
				player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message);

				GameController.instance.Notify (player, GameEvent.Henchmen_RemovedFromHireable);

				break;
			}
		}
	}
}
