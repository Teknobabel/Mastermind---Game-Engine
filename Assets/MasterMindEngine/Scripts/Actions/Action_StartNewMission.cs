using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_StartNewMission : Action {

	public MissionPlan m_missionPlan;

	public int m_playerID = -1;

	public override void ExecuteAction ()
	{
		Player player = GameEngine.instance.game.playerList [m_playerID];
		player.AddMission (m_missionPlan);
		m_missionPlan.m_state = MissionPlan.State.Active;
		m_missionPlan.m_floorSlot.m_state = Lair.FloorSlot.FloorState.MissionInProgress;
		m_missionPlan.m_new = true;

//		string title = "New Mission Begins";
//		string message = "Mission: " + m_missionPlan.m_currentMission.m_name + " is now underway.";
//
//		player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message);

//		foreach (Player.ActorSlot aSlot in m_missionPlan.m_actorSlots) {
//
//			if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {
//
//				aSlot.m_actor.notifications.AddNotification(GameController.instance.GetTurnNumber(), title, message);
//			}
//		}

		// notify UI for updating

		GameController.instance.Notify (player, GameEvent.Player_NewMissionStarted);
	}
}
