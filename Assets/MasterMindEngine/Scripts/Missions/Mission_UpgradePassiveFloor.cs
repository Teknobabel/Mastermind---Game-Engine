using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_UpgradePassiveFloor : Mission {

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		Player player = GameEngine.instance.game.playerList [0];

		if (plan.m_result == MissionPlan.Result.Success) {

			plan.m_floorSlot.m_floor.Initialize ();

		}
	}
}
