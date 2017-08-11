using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_RevealMultipleAssets : Mission {

	public int m_numAssets = 1;
	public float m_revealChance = 1.0f;

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		Player player = GameEngine.instance.game.playerList [0];

		if (plan.m_result == MissionPlan.Result.Success) {

			List<Site.AssetSlot> hiddenAssets = new List<Site.AssetSlot> ();

			foreach (Site.AssetSlot aSlot in plan.m_missionSite.assets) {

				if (aSlot.m_state == Site.AssetSlot.State.Hidden) {

					hiddenAssets.Add (aSlot);
				}
			}
				
			bool firstAsset = true;

			for (int i = 0; i < m_numAssets-1; i++)
			{
				if (hiddenAssets.Count > 0) {

					Site.AssetSlot aSlot = hiddenAssets [0];
					hiddenAssets.RemoveAt (0);

					if (Random.Range (0.0f, 1.0f) <= m_revealChance || firstAsset) {

						Action_RevealAsset revealAsset = new Action_RevealAsset ();
						revealAsset.m_playerID = 0;
						revealAsset.m_assetSlot = aSlot;
						GameController.instance.ProcessAction (revealAsset);

						firstAsset = false;
					}

				} else {
					
					i = 99;
				}
			}
		}
	}
}
