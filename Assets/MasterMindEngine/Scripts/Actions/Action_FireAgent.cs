﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_FireAgent : Action {

	public int m_playerNumber = -1;
	public int m_henchmenID = -1;

	public override void ExecuteAction ()
	{
		Actor henchmen = GameController.instance.GetActor (m_henchmenID);

		Debug.Log ("Firing Henchmen: " + henchmen.m_actorName);

		if (GameEngine.instance.game.playerList.ContainsKey (m_playerNumber)) {

			Player player = GameEngine.instance.game.playerList [m_playerNumber];

			// remove from any in planning missions

			foreach (Lair.FloorSlot fSlot in player.lair.floorSlots) {

				if (fSlot.m_state != Lair.FloorSlot.FloorState.MissionInProgress) {

					for (int i = 0; i < fSlot.m_missionPlan.m_actorSlots.Count; i++) {

						Player.ActorSlot aSlot = fSlot.m_missionPlan.m_actorSlots[i];

						if (aSlot.m_actor.id == m_henchmenID) {

							fSlot.m_missionPlan.m_actorSlots.RemoveAt (i);
							break;
						}
					}
				}
			}

			// remove from henchmen pool

			foreach (Player.ActorSlot newHenchmenSlot in player.henchmenPool.m_henchmenSlots) {

				if (newHenchmenSlot.m_state != Player.ActorSlot.ActorSlotState.Empty && newHenchmenSlot.m_actor.id == henchmen.id) {

					newHenchmenSlot.RemoveHenchmen ();

					string title = "Henchmen Fired";
					string message = henchmen.m_actorName + " has left your organization.";

					player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Contacts, true, m_missionID);
					henchmen.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Contacts, false, m_missionID);


					// notify UI for updating

					GameController.instance.Notify (player, GameEvent.Player_HenchmenPoolChanged);

					break;
				}
			}

			// add back to available pool

			player.hiringPool.m_availableHenchmen.Add (henchmen);

		} else {
			Debug.Log ("Player ID not found");
		}
	}
}
