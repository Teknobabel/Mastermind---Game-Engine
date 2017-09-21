using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_ChangeRegionAlertLevel : Mission {

	public int m_alertChange = -1;
	public float m_alertChangeChance = 0.4f;

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		if (plan.m_result == MissionPlan.Result.Success) {

			bool first = true;

			List<Site> sites = new List<Site> ();

			foreach (KeyValuePair<int, Site> pair in GameController.instance.game.siteList)
			{
				sites.Add (pair.Value);
			}

			while (sites.Count > 0)
			{
				int r = Random.Range (0, sites.Count);
				Site thisSite = sites [r];
				sites.RemoveAt (r);

				bool doChangeAlert = false;

				if (first) {

					doChangeAlert = true;
					first = false;
				} else if (Random.Range (0.0f, 1.0f) <= m_alertChangeChance) {

					doChangeAlert = true;
				}

				if (doChangeAlert) {
					Action_ChangeAlertLevel changeAlertLevel = new Action_ChangeAlertLevel ();
					changeAlertLevel.m_playerID = 0;
					changeAlertLevel.m_siteID = thisSite.id;
					changeAlertLevel.m_amount = m_alertChange;
					GameController.instance.ProcessAction (changeAlertLevel);
				}
			}
		}
	}
}
