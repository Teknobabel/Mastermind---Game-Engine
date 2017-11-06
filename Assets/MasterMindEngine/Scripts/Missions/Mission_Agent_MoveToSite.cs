using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_Agent_MoveToSite : Mission {

	public override void CompleteMission (MissionPlan plan)
	{
		Debug.Log ("Moving Agent to new Site");

		base.CompleteMission (plan);

		if (plan.m_targetActor.m_currentSite != null) {

			plan.m_targetActor.m_currentSite.RemoveAgent (plan.m_targetActor);
		}

		plan.m_missionSite.AddAgent (plan.m_targetActor);
	}
}
