using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_AddSlotToFLoor : Mission {

	public int m_numSlots = 1;

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

		Player player = GameController.instance.game.playerList [0];
		int turnNum = GameController.instance.game.currentTurn;

		if (plan.m_result == MissionPlan.Result.Success) {

			plan.m_floorSlot.m_numActorSlots += m_numSlots;
//			plan.m_floorSlot.m_floor.level++;

			string title = "Floor Level Increased";
			string message = plan.m_floorSlot.m_floor.m_name + " is now Level " + plan.m_floorSlot.m_floor.level.ToString ();
			player.notifications.AddNotification (turnNum, title, message, EventLocation.Lair, false, plan.m_missionID);

			if (m_numSlots > 0) {
				
				title = "New Henchmen Slot Available";
				message = "An additional Henchmen can assist with Missions at " + plan.m_floorSlot.m_floor.m_name;
				player.notifications.AddNotification (turnNum, title, message, EventLocation.Lair, false, plan.m_missionID);
			}

			// find newly unlocked missions

			List<Mission> newMissions = new List<Mission> ();

			foreach (Mission m in plan.m_floorSlot.m_floor.m_missions) {

				if (m.m_minFloorLevel == plan.m_floorSlot.m_floor.level) {

					newMissions.Add (m);
				}
			}

			foreach (Mission m in newMissions) {

				title = "New Mission Available";
				message = "Mission: " + m.m_name + " is now available";
				player.notifications.AddNotification (turnNum, title, message, EventLocation.Lair, false, plan.m_missionID);

			}

			plan.m_floorSlot.m_floor.completedUpgrades.Add (this);
		}
	}
}
