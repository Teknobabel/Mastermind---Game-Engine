using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_GainRandomAsset : Mission {

	public Asset[] m_assets;
	public int m_numAssets;

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		if (plan.m_result == MissionPlan.Result.Success) {

			Player player = GameEngine.instance.game.playerList [0];

			for (int i = 0; i < m_numAssets; i++) {

				Asset asset = m_assets[Random.Range(0, m_assets.Length)];

				Action_GainAsset gainAsset = new Action_GainAsset ();
				gainAsset.m_asset = asset;
				gainAsset.m_player = player;
				GameController.instance.ProcessAction (gainAsset);
			}


		}

	}
}
