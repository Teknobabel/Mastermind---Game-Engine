using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_StartNewMission : Action {

	public MissionPlan m_missionPlan;

	public int m_playerID = -1;

	public override void ExecuteAction ()
	{
		m_missionPlan.m_missionID = GameEngine.instance.game.GetID ();
		Player player = GameEngine.instance.game.playerList [m_playerID];
		player.AddMission (m_missionPlan);
		m_missionPlan.m_state = MissionPlan.State.Active;

		if (m_missionPlan.m_floorSlot != null) {
			m_missionPlan.m_floorSlot.m_state = Lair.FloorSlot.FloorState.MissionInProgress;
		} else if (m_missionPlan.m_goal != null) {
			m_missionPlan.m_goal.m_state = OmegaPlan.OPGoal.State.InProgress;
		}

		foreach (Site.AssetSlot aSlot in m_missionPlan.m_linkedPlayerAssets) {

			if (aSlot.m_state != Site.AssetSlot.State.InUse) {
				aSlot.m_state = Site.AssetSlot.State.InUse;
			} else {
				Debug.Log("Asset: " + aSlot.m_asset.m_name + " already in use");
			}
		}

		foreach (Player.ActorSlot aSlot in m_missionPlan.m_actorSlots) {

			aSlot.m_state = Player.ActorSlot.ActorSlotState.OnMission;
		}

//		if (m_missionPlan.m_floorSlot != null) {
//			
//			foreach (Player.ActorSlot aSlot in m_missionPlan.m_floorSlot.m_actorSlots) {
//
//				aSlot.m_state = Player.ActorSlot.ActorSlotState.OnMission;
//			}
//		} else {
//
//			foreach (Player.ActorSlot aSlot in m_missionPlan.m_goal.plan.m_actorSlots) {
//
//				aSlot.m_state = Player.ActorSlot.ActorSlotState.OnMission;
//			}
//		}

		m_missionPlan.m_new = true;

		string title = "New Mission Begins";
		string message = "Mission: " + m_missionPlan.m_currentMission.m_name + " is now underway.";

		player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Missions, true, m_missionPlan.m_missionID);

		foreach (Player.ActorSlot aSlot in m_missionPlan.m_actorSlots) {

			if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {

				aSlot.m_actor.notifications.AddNotification(GameController.instance.GetTurnNumber(), title, message, EventLocation.Missions, false, m_missionPlan.m_missionID);
			}
		}

		// notify UI for updating

		GameController.instance.Notify (player, GameEvent.Player_NewMissionStarted);
	}
}
