using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_StealAsset : Mission {

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
			gainAsset.m_missionID = plan.m_missionID;
			GameController.instance.ProcessAction (gainAsset);

		}
	}
}
