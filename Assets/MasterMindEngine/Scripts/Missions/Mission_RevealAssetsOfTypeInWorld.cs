using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_RevealAssetsOfTypeInWorld : Mission {

	public Asset m_assetType;
	public int m_numToReveal = 1;
	public float m_revealChance = 0.65f;

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		if (plan.m_result == MissionPlan.Result.Success) {

			List<Site.AssetSlot> hiddenAssets = new List<Site.AssetSlot> ();

			foreach (KeyValuePair<int, Site> pair in GameEngine.instance.game.siteList) {

				foreach (Site.AssetSlot aSlot in pair.Value.assets) {

					if (aSlot.m_state == Site.AssetSlot.State.Hidden && aSlot.m_asset.m_name == m_assetType.m_name) {

						hiddenAssets.Add (aSlot);


					}
				}
			}

			if (hiddenAssets.Count == 0) {

				// message that no asset of type was found
			}

			bool first = true;

			for (int i = 0; i < m_numToReveal; i++) {

				if (hiddenAssets.Count > 0) {

					int rand = Random.Range (0, hiddenAssets.Count);
					Site.AssetSlot aSlot = hiddenAssets [rand];
					hiddenAssets.RemoveAt (rand);

					bool doRevealAsset = false;

					if (first) {

						doRevealAsset = true;
						first = false;

					} else if (Random.Range (0.0f, 1.0f) <= m_revealChance) {

						doRevealAsset = true;
					}

					if (doRevealAsset) {

						Action_RevealAsset revealAsset = new Action_RevealAsset ();
						revealAsset.m_playerID = 0;
						revealAsset.m_missionID = plan.m_missionID;
						revealAsset.m_assetSlot = aSlot;
						GameController.instance.ProcessAction (revealAsset);

					}
				} else {

					break;
				}
			}
		}
	}
}
