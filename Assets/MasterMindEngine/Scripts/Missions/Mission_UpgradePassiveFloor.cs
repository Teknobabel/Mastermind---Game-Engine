using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_UpgradePassiveFloor : Mission {

	public override bool IsValid (MissionPlan plan)
	{
		if (plan.m_floorSlot != null) {

			foreach (Mission m in plan.m_floorSlot.m_floor.completedUpgrades) {

				if (m.m_name == m_name) {

					return false;
				}
			}
		}

		bool meetsLevelReq = base.IsValid (plan);
		return meetsLevelReq;
	}

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		Player player = GameEngine.instance.game.playerList [0];

		if (plan.m_result == MissionPlan.Result.Success) {

			plan.m_floorSlot.m_floor.Initialize ();
			plan.m_floorSlot.m_floor.level++;
			plan.m_floorSlot.m_floor.completedUpgrades.Add (this);

		}
	}
}
