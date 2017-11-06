using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_HireAgent : Action {

	public int m_playerNumber = -1;
	public int m_henchmenID = -1;

	public override void ExecuteAction ()
	{
		Actor henchmen = GameController.instance.GetActor (m_henchmenID);

		Debug.Log ("Hiring new Henchmen: " + henchmen.m_actorName);

		Player player = null;
		bool displayToast = true;
		bool agent = false;

		if (GameEngine.instance.game.playerList.ContainsKey (m_playerNumber)) {

			player = GameEngine.instance.game.playerList [m_playerNumber];
		} else if (GameEngine.instance.game.agentPlayerList.ContainsKey (m_playerNumber)) {
			player = GameEngine.instance.game.agentPlayerList [m_playerNumber];
			displayToast = false;
			agent = true;
		}

		if (player != null) {

			// remove from available pool if present

			if (player.hiringPool.m_availableHenchmen.Contains (henchmen)) {

				player.hiringPool.m_availableHenchmen.Remove (henchmen);
			}

			// remove from hiring pool if present

			foreach (Player.ActorSlot aSlot in player.hiringPool.m_hireSlots) {

				if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty && aSlot.m_actor.id == m_henchmenID) {

					//					Actor thisHenchmen = aSlot.m_actor;

					aSlot.RemoveHenchmen ();

					break;
				}
			}

			// add to henchmen pool

			foreach (Player.ActorSlot newHenchmenSlot in player.henchmenPool.m_henchmenSlots) {

				if (newHenchmenSlot.m_state == Player.ActorSlot.ActorSlotState.Empty) {

					newHenchmenSlot.SetHenchmen (henchmen);

					if (agent) {

						newHenchmenSlot.m_visibilityState = Player.ActorSlot.VisibilityState.Hidden;
					}

					string title = "New Henchmen Hired";
					string message = henchmen.m_actorName + " has joined your organization.";

					player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Contacts, displayToast, m_missionID);
					henchmen.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Contacts, false, m_missionID);


					// notify UI for updating

					if (player.id == 0) {

						GameController.instance.Notify (player, GameEvent.Player_HiringPoolChanged);
						GameController.instance.Notify (player, GameEvent.Player_HenchmenPoolChanged);
					}

					break;
				}
			}

		} else {
			Debug.Log ("Player ID not found");
		}
	}
}
