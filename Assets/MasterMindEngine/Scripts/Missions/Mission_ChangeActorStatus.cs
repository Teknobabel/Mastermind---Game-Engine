using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_ChangeActorStatus : Mission {

	public StatusTrait m_currentStatus;
	public StatusTrait m_newStatus;

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		if (plan.m_result == MissionPlan.Result.Success) {

			if (m_currentStatus == null || plan.m_targetActor.m_actor.m_status == m_currentStatus) {

				plan.m_targetActor.m_actor.m_status = m_newStatus;

				string title = "Status Change";
				string message = plan.m_targetActor.m_actor.m_actorName + "'s Status is now " + m_newStatus.m_name;
				Player player = GameEngine.instance.game.playerList [0];
				player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Contacts);
				plan.m_targetActor.m_actor.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Contacts);
			}
		
		}
	}
}
