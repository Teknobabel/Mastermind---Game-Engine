using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_GainAsset : Mission {

	public Asset m_asset;

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		if (plan.m_result == MissionPlan.Result.Success) {

			Player player = GameEngine.instance.game.playerList [0];

			Action_GainAsset gainAsset = new Action_GainAsset ();
			gainAsset.m_asset = m_asset;
			gainAsset.m_player = player;
			gainAsset.m_missionID = plan.m_missionID;
			GameController.instance.ProcessAction (gainAsset);

		}
	}
}
