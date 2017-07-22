using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_BuildFloor : Mission {

	public Floor m_floor;

	public override void CompleteMission (MissionPlan plan)
	{
		Player player = GameEngine.instance.game.playerList [0];

		string title = "Mission Completed";
		string message = "Mission: " + plan.m_currentMission.m_name;

		if (plan.m_result == MissionPlan.Result.Success) {

			message += " is a success!";

		} else if (plan.m_result == MissionPlan.Result.Fail) {

			message += " is a failure.";

		}

		player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message);

		foreach (Player.ActorSlot aSlot in plan.m_actorSlots) {

			if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {

				aSlot.m_actor.notifications.AddNotification(GameController.instance.GetTurnNumber(), title, message);
			}
		}

		if (plan.m_result == MissionPlan.Result.Success) {

			Action_BuildNewFloor buildFloor = new Action_BuildNewFloor ();
			buildFloor.m_floor = m_floor;
			buildFloor.m_player = GameEngine.instance.game.playerList [0];
			GameController.instance.ProcessAction (buildFloor);

		}
	}
}