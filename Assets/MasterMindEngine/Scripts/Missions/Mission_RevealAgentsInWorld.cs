using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_RevealAgentsInWorld : Mission {

	public int numAgents = 1;

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		List<Player.ActorSlot> validTargets = new List<Player.ActorSlot> ();

		foreach (KeyValuePair<int, Site> pair in GameController.instance.game.siteList) {

			if (pair.Value.agents.Count > 0) {

				foreach (Player.ActorSlot aSlot in pair.Value.agents) {

					if (aSlot.m_visibilityState == Player.ActorSlot.VisibilityState.Hidden) {

						validTargets.Add (aSlot);
					}
				}
			}
		}

		for (int i = 0; i < numAgents; i++) {

			if (validTargets.Count > 0) {

				int r = Random.Range (0, validTargets.Count);

				Player.ActorSlot target = validTargets [r];
				validTargets.RemoveAt (r);

				Action_ChangeAgentVisibilityState revealAgent = new Action_ChangeAgentVisibilityState ();
				revealAgent.m_agentSlot = target;
				revealAgent.m_newVisibilityState = Player.ActorSlot.VisibilityState.Visible;
				revealAgent.m_missionID = plan.m_missionID;
				GameController.instance.ProcessAction (revealAgent);
			}
		}
	}
}
