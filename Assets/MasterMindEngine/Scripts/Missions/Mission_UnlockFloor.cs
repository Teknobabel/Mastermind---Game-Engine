using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_UnlockFloor : Mission {

	public Mission_BuildFloor m_floorPlan;

	public override bool IsValid (MissionPlan plan)
	{
		Lair l = GameController.instance.GetLair (0);

		foreach (Lair.FloorSlot fSlot in l.floorSlots) {

			if (fSlot.m_state != Lair.FloorSlot.FloorState.Empty && fSlot.m_floor.m_name == m_floorPlan.m_floor.m_name) {

				return false;
			}
		}

		bool meetsLevelReq = base.IsValid (plan);
		return meetsLevelReq;
	}

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		Player player = GameEngine.instance.game.playerList [0];
		Lair lair = GameController.instance.GetLair(0);
		lair.unlockedFacilities.Add (m_floorPlan);

		string title = "New Facility Available";
		string message = m_floorPlan.m_floor.m_name + " can now be added to your Lair.";
		player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Lair, false, plan.m_missionID);

	}
}
