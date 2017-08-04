using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_RemoveSiteTrait : Mission {

//	public int m_siteID = -1;
//	public SiteTrait m_trait;

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		if (plan.m_result == MissionPlan.Result.Success) {

			Action_RemoveSiteTrait removeTrait = new Action_RemoveSiteTrait ();
			removeTrait.m_siteID = plan.m_missionSite.id;
			removeTrait.m_trait = plan.m_targetSiteTrait;
			GameController.instance.ProcessAction (removeTrait);
//
////			Site site = GameController.instance.GetSite (m_siteID);
//			plan.m_missionSite.RemoveTrait (plan.m_targetSiteTrait);
//
//			string title = "Trait Lost";
//			string message = "\n" + plan.m_missionSite.m_siteName + " lost the " + plan.m_targetSiteTrait.m_name + " Trait";
//			Player player = GameEngine.instance.game.playerList [0];
//			player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message);
		}
	}
}
