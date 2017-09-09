using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_RevealAssetInWorld : Mission {

	public int m_numAssets = 1;
	public float m_revealChance = 1.0f;

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		if (plan.m_result == MissionPlan.Result.Success) {

			List<Site.AssetSlot> hiddenAssets = new List<Site.AssetSlot> ();


			foreach (KeyValuePair<int, Site> pair in GameEngine.instance.game.siteList) {
				
				foreach (Site.AssetSlot aSlot in pair.Value.assets) {

					if (aSlot.m_state == Site.AssetSlot.State.Hidden) {

						// lower rank assets are more likely to be discovered

						int numTimesToAdd = 1;

						switch (aSlot.m_asset.m_rank) {
						case 1:
							numTimesToAdd = 4;
							break;
						case 2:
							numTimesToAdd = 3;
							break;
						case 3:
							numTimesToAdd = 2;
							break;
						}

						switch (pair.Value.m_maxAlertLevel) {
						case 1:
							numTimesToAdd += 3;
							break;
						case 2:
							numTimesToAdd += 2;
							break;
						case 3:
							numTimesToAdd += 1;
							break;
						}

						switch (pair.Value.traits.Count) {
						case 0:
							numTimesToAdd += 4;
							break;
						case 1:
							numTimesToAdd += 2;
							break;
						case 2:
							numTimesToAdd += 1;
							break;
						}
							
						for (int i = 0; i < numTimesToAdd; i++) {

							hiddenAssets.Add (aSlot);
						}
					}
				}
			}

			if (hiddenAssets.Count > 0) {

				//				title = "Asset Revealed";

				List<Site.AssetSlot> pickedAssets = new List<Site.AssetSlot> ();

				for (int i = 0; i < m_numAssets; i++) {

					if (i == 0 || Random.Range (0.0f, 1.0f) <= m_revealChance) {
						
						int rand = Random.Range (0, hiddenAssets.Count);
						Site.AssetSlot aSlot = hiddenAssets [rand];

						if (!pickedAssets.Contains (aSlot)) {

							pickedAssets.Add (aSlot);
						}
					}
				}

				foreach (Site.AssetSlot aSlot in pickedAssets) {

					Action_RevealAsset revealAsset = new Action_RevealAsset ();
					revealAsset.m_playerID = 0;
					revealAsset.m_missionID = plan.m_missionID;
					revealAsset.m_assetSlot = aSlot;
					GameController.instance.ProcessAction (revealAsset);
				}

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
