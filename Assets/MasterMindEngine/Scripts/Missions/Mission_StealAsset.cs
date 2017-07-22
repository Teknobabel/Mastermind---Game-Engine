﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_StealAsset : Mission {

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		Player player = GameEngine.instance.game.playerList [0];

		string title = "Mission Completed";
		string message = "Mission: " + plan.m_currentMission.m_name;

		if (plan.m_result == MissionPlan.Result.Success) {

			message += " is a success!";

		} else if (plan.m_result == MissionPlan.Result.Fail) {

			message += " is a failure.";

		}

		player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message);

		foreach (Player.ActorSlot aSlot in plan.m_actorSlots) {

			if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {

				aSlot.m_actor.notifications.AddNotification(GameController.instance.GetTurnNumber(), title, message);
			}
		}

		if (plan.m_result == MissionPlan.Result.Success && plan.m_currentAsset.m_state == Site.AssetSlot.State.Revealed) {

			Action_GainAsset gainAsset = new Action_GainAsset ();
			gainAsset.m_asset = plan.m_currentAsset.m_asset;
			gainAsset.m_player = player;
			GameController.instance.ProcessAction (gainAsset);

			plan.m_missionSite.RemoveAsset (plan.m_currentAsset);

		}
	}
}