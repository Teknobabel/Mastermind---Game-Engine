using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_IncreaseBaseFloors : Mission {

	public int m_numFloors = 1;

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

		if (plan.m_result == MissionPlan.Result.Success) {

			Player player = GameEngine.instance.game.playerList [0];

			Lair lair = GameController.instance.GetLair (0);

			lair.maxFloors += m_numFloors;

			string title = "Lair Expanded";
			string message = "Your Lair can now hold " + m_numFloors.ToString() + " more Facilities";
			player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Lair, false, plan.m_missionID);
		}
	}
}
