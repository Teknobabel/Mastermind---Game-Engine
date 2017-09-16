using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_StealRandomSiteAsset : Mission {

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		if (plan.m_result == MissionPlan.Result.Success && plan.m_missionSite != null) {

			Player player = GameEngine.instance.game.playerList [0];

			List<Site.AssetSlot> validAssets = new List<Site.AssetSlot> ();

			foreach (Site.AssetSlot aSlot in plan.m_missionSite.assets) {

				if (aSlot.m_state == Site.AssetSlot.State.Revealed) {

					validAssets.Add (aSlot);
				}
			}

			if (validAssets.Count > 0) {

				Site.AssetSlot randAsset = validAssets[Random.Range(0, validAssets.Count)];
				Asset asset = randAsset.m_asset;
				plan.m_missionSite.RemoveAsset (randAsset);

				Action_GainAsset gainAsset = new Action_GainAsset ();
				gainAsset.m_asset = asset;
				gainAsset.m_player = player;
				gainAsset.m_missionID = plan.m_missionID;
				GameController.instance.ProcessAction (gainAsset);

			}
		}
	}
}
