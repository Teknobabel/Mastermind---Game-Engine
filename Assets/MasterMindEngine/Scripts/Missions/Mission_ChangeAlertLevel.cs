using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_ChangeAlertLevel : Mission {

	public int m_alertChange = -1;

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		if (plan.m_result == MissionPlan.Result.Success) {

			Action_ChangeAlertLevel changeAlertLevel = new Action_ChangeAlertLevel ();
			changeAlertLevel.m_playerID = 0;
			changeAlertLevel.m_siteID = plan.m_missionSite.id;
			changeAlertLevel.m_amount = m_alertChange;
			GameController.instance.ProcessAction (changeAlertLevel);
		}
	}
}
