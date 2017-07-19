using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_EvaluateMission : Action {

	public MissionPlan m_missionPlan;

	public int m_playerID = -1;

	public override void ExecuteAction ()
	{

		m_missionPlan.m_turnNumber++;

		Player player = GameEngine.instance.game.playerList [m_playerID];

		if (m_missionPlan.m_turnNumber >= m_missionPlan.m_currentMission.m_duration) {

			// complete mission

			m_missionPlan.m_state = MissionPlan.State.Complete;

			string title = "Mission Completed";
			string message = "Mission: " + m_missionPlan.m_currentMission.m_name + " is complete.";

			player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message);

			foreach (Player.ActorSlot aSlot in m_missionPlan.m_actorSlots) {
	
				if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {
	
					aSlot.m_actor.notifications.AddNotification(GameController.instance.GetTurnNumber(), title, message);
				}

				aSlot.RemoveHenchmen ();
			}

			m_missionPlan.m_currentMission = null;
			m_missionPlan.m_missionSite = null;
			m_missionPlan.m_currentAsset = null;
			m_missionPlan.m_floorSlot.m_state = Lair.FloorSlot.FloorState.Idle;

		} else {

			string title = "Mission Continues";
			string message = "Mission: " + m_missionPlan.m_currentMission.m_name + " is in progress.";

			player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message);

			foreach (Player.ActorSlot aSlot in m_missionPlan.m_actorSlots) {

				if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {

					aSlot.m_actor.notifications.AddNotification(GameController.instance.GetTurnNumber(), title, message);
				}
			}
		}

//		Player player = GameEngine.instance.game.playerList [m_playerID];
//		player.AddMission (m_missionPlan);
//
//		string title = "New Mission Begins";
//		string message = "Mission: " + m_missionPlan.m_currentMission.m_name + " is now underway.";
//
//		player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message);
//
//		foreach (Player.ActorSlot aSlot in m_missionPlan.m_actorSlots) {
//
//			if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {
//
//				aSlot.m_actor.notifications.AddNotification(GameController.instance.GetTurnNumber(), title, message);
//			}
//		}
	}
}
