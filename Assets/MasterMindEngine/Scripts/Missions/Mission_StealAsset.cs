using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_StealAsset : Mission {

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		if (plan.m_result == MissionPlan.Result.Success && plan.m_currentAsset.m_state == Site.AssetSlot.State.Revealed) {

			Player player = GameEngine.instance.game.playerList [0];

			if (plan.m_currentAsset.m_site != null) {

				plan.m_currentAsset.m_site.RemoveAsset (plan.m_currentAsset);
			}

			Action_GainAsset gainAsset = new Action_GainAsset ();
			gainAsset.m_asset = plan.m_currentAsset.m_asset;
			gainAsset.m_player = player;
			GameController.instance.ProcessAction (gainAsset);

//			plan.m_missionSite.RemoveAsset (plan.m_currentAsset);

//			plan.m_missionSite.UpdateAlert (1);
//			Action_ChangeAlertLevel increaseAlertLevel = new Action_ChangeAlertLevel();
//			increaseAlertLevel.m_playerID = 0;
//			increaseAlertLevel.m_amount = 1;
//			increaseAlertLevel.m_siteID = plan.m_missionSite.id;
//			GameController.instance.ProcessAction (increaseAlertLevel);

		}
	}
}
