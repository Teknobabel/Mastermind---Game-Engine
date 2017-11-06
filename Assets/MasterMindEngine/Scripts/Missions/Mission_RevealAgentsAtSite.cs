using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_RevealAgentsAtSite : Mission {

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		if (plan.m_missionSite != null) {

			if (plan.m_missionSite.agents.Count > 0) {

				foreach (Player.ActorSlot aSlot in plan.m_missionSite.agents) {

					if (aSlot.m_visibilityState == Player.ActorSlot.VisibilityState.Hidden) {

						Action_ChangeAgentVisibilityState revealAgent = new Action_ChangeAgentVisibilityState ();
						revealAgent.m_agentSlot = aSlot;
						revealAgent.m_newVisibilityState = Player.ActorSlot.VisibilityState.Visible;
						revealAgent.m_missionID = plan.m_missionID;
						GameController.instance.ProcessAction (revealAgent);
					}
				}

			} else {

			}
		}
	}
}
