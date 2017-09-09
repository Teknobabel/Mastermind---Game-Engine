using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_CancelMission : Action {

	public MissionPlan m_missionPlan;

	public int m_playerID = -1;

	public override void ExecuteAction ()
	{
		Player player = GameEngine.instance.game.playerList [m_playerID];

		if (m_missionPlan.m_turnNumber > 0) {

			string title = "Mission Cancelled";
			string message = "Mission: " + m_missionPlan.m_currentMission.m_name + " has been cancelled.";

			player.notifications.AddNotification (GameController.instance.GetTurnNumber (), title, message, EventLocation.Missions, true, m_missionID);

			foreach (Player.ActorSlot aSlot in m_missionPlan.m_actorSlots) {

				if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {

					aSlot.m_actor.notifications.AddNotification (GameController.instance.GetTurnNumber (), title, message, EventLocation.Missions, false, m_missionID);
				}
			}
		}

		// free up participating henchmen

		foreach (Player.ActorSlot aSlot in m_missionPlan.m_actorSlots) {

			if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {

				aSlot.m_state = Player.ActorSlot.ActorSlotState.Active;
			}
		}

		m_missionPlan.m_actorSlots.Clear ();

		// free up any assets in use

		while (m_missionPlan.m_linkedPlayerAssets.Count > 0) {

			Site.AssetSlot aSlot = m_missionPlan.m_linkedPlayerAssets [0];
			m_missionPlan.m_linkedPlayerAssets.RemoveAt (0);
			aSlot.m_state = Site.AssetSlot.State.Revealed;
		}

		// remove from player active mission list

		player.RemoveMission (m_missionPlan);

		// reset values if lair floor mission

		if (m_missionPlan.m_floorSlot != null) {

			m_missionPlan.m_currentMission = null;
			m_missionPlan.m_missionSite = null;
			m_missionPlan.m_currentAsset = null;
			m_missionPlan.m_new = false;
			m_missionPlan.m_successChance = 0;
			m_missionPlan.m_turnNumber = 0;
			//			m_missionPlan.m_result = MissionPlan.Result.None;
			m_missionPlan.m_state = MissionPlan.State.Planning;
			m_missionPlan.m_requiredTraits.Clear ();
			m_missionPlan.m_matchingTraits.Clear ();
			m_missionPlan.m_floorSlot.m_actorSlots.Clear ();

			m_missionPlan.m_floorSlot.m_state = Lair.FloorSlot.FloorState.Idle;
		}

		// reset the OP mission if OP mission

		if (m_missionPlan.m_goal != null && m_missionPlan.m_goal.m_state != OmegaPlan.OPGoal.State.Complete) {

			m_missionPlan.m_goal.m_state = OmegaPlan.OPGoal.State.Incomplete;
		}

		GameController.instance.Notify (player, GameEvent.Player_MissionCancelled);
	}
}
