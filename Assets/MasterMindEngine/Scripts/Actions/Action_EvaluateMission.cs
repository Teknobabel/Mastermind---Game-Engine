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

			// determine success or failure

			float successChance = (float)m_missionPlan.m_successChance;

			if (Random.Range (1.0f, 100.0f) <= successChance) {

				// mission successful

				m_missionPlan.m_result = MissionPlan.Result.Success;

			} else {

				// mission failure

				m_missionPlan.m_result = MissionPlan.Result.Fail;
			}

			if (GameEngine.instance.m_forceMissionSuccess) {
				m_missionPlan.m_result = MissionPlan.Result.Success;
			}

			m_missionPlan.m_currentMission.CompleteMission (m_missionPlan);

			foreach (Player.ActorSlot aSlot in m_missionPlan.m_actorSlots) {

				aSlot.RemoveHenchmen ();
			}

			m_missionPlan.m_currentMission = null;
			m_missionPlan.m_missionSite = null;
			m_missionPlan.m_currentAsset = null;
			m_missionPlan.m_new = false;
			m_missionPlan.m_successChance = 0;
			m_missionPlan.m_turnNumber = 0;
			m_missionPlan.m_result = MissionPlan.Result.None;
//			m_missionPlan.m_state = MissionPlan.State.Planning;
			m_missionPlan.m_requiredTraits.Clear ();
			m_missionPlan.m_matchingTraits.Clear ();

			if (m_missionPlan.m_floorSlot != null) {
				m_missionPlan.m_floorSlot.m_state = Lair.FloorSlot.FloorState.Idle;
			}

			GameController.instance.Notify (player, GameEvent.Player_MissionCompleted);

		} else {

			string title = "Mission Continues";
			string message = "Mission: " + m_missionPlan.m_currentMission.m_name + " is in progress.";

			if (m_missionPlan.m_turnNumber == 1) {

				title = "New Mission Begins";
				message = "Mission: " + m_missionPlan.m_currentMission.m_name + " is underway.";
			}

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
