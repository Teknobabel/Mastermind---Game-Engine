using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_StealAndRevealAssets : Mission {

	public int m_numAssets = 1;
	public float m_revealChance = 1.0f;

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		if (plan.m_result == MissionPlan.Result.Success && plan.m_currentAsset.m_state == Site.AssetSlot.State.Revealed) {

			Player player = GameEngine.instance.game.playerList [0];

			Asset asset = plan.m_currentAsset.m_asset;

			if (plan.m_currentAsset.m_site != null) {

				plan.m_currentAsset.m_site.RemoveAsset (plan.m_currentAsset);
			}

			Action_GainAsset gainAsset = new Action_GainAsset ();
			gainAsset.m_asset = asset;
			gainAsset.m_player = player;
			GameController.instance.ProcessAction (gainAsset);

			Action_ChangeAlertLevel increaseAlertLevel = new Action_ChangeAlertLevel();
			increaseAlertLevel.m_playerID = 0;
			increaseAlertLevel.m_amount = 1;
			increaseAlertLevel.m_siteID = plan.m_missionSite.id;
			GameController.instance.ProcessAction (increaseAlertLevel);


			// chance to reveal remaining assets

			List<Site.AssetSlot> hiddenAssets = new List<Site.AssetSlot> ();

			foreach (Site.AssetSlot aSlot in plan.m_missionSite.assets) {

				if (aSlot.m_state == Site.AssetSlot.State.Hidden) {

					hiddenAssets.Add (aSlot);
				}
			}

			for (int i = 0; i < m_numAssets-1; i++)
			{
				if (hiddenAssets.Count > 0) {

					Site.AssetSlot aSlot = hiddenAssets [0];
					hiddenAssets.RemoveAt (0);

					if (Random.Range (0.0f, 1.0f) <= m_revealChance) {

						Action_RevealAsset revealAsset = new Action_RevealAsset ();
						revealAsset.m_playerID = 0;
						revealAsset.m_assetSlot = aSlot;
						GameController.instance.ProcessAction (revealAsset);

					}

				} else {

					i = 99;
				}
			}
		}
	}
}
