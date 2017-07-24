using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_GainTrait : Mission {

	public Trait m_trait;

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

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

		if (plan.m_result == MissionPlan.Result.Success && plan.m_targetActor.m_state != Player.ActorSlot.ActorSlotState.Empty) {

			Action_GainTrait gainTrait = new Action_GainTrait ();
			gainTrait.m_playerID = 0;
			gainTrait.m_actor = plan.m_targetActor.m_actor;
			gainTrait.m_newTrait = m_trait;
			GameController.instance.ProcessAction (gainTrait);
		}
	}
}
