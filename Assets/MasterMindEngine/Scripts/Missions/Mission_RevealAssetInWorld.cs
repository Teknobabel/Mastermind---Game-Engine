using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_RevealAssetInWorld : Mission {

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		if (plan.m_result == MissionPlan.Result.Success) {

			List<Site.AssetSlot> hiddenAssets = new List<Site.AssetSlot> ();


			foreach (KeyValuePair<int, Site> pair in GameEngine.instance.game.siteList) {
				
				foreach (Site.AssetSlot aSlot in pair.Value.assets) {

					if (aSlot.m_state == Site.AssetSlot.State.Hidden) {

						hiddenAssets.Add (aSlot);
					}
				}
			}

			if (hiddenAssets.Count > 0) {

				//				title = "Asset Revealed";

				int rand = Random.Range (0, hiddenAssets.Count);

				Site.AssetSlot aSlot = hiddenAssets [rand];

				Action_RevealAsset revealAsset = new Action_RevealAsset ();
				revealAsset.m_playerID = 0;
				revealAsset.m_assetSlot = aSlot;
				GameController.instance.ProcessAction (revealAsset);

				//				aSlot.m_state = Site.AssetSlot.State.Revealed;

				//				message += "\n" + aSlot.m_asset.m_name + " is revealed in " + plan.m_missionSite.m_siteName + ".";

			} else {

//				string title = "No Assets Found";
//				string message = "\nThere are no hidden Assets to reveal";
//				player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message);
			}

		} else if (plan.m_result == MissionPlan.Result.Fail) {

			//			plan.m_missionSite.UpdateAlert (2);
//			Action_ChangeAlertLevel increaseAlertLevel = new Action_ChangeAlertLevel();
//			increaseAlertLevel.m_playerID = 0;
//			increaseAlertLevel.m_amount = 2;
//			increaseAlertLevel.m_siteID = plan.m_missionSite.id;
//			GameController.instance.ProcessAction (increaseAlertLevel);

		}
	}
}
