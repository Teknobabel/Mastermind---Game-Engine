using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_RevealAsset : Mission {

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		Player player = GameEngine.instance.game.playerList [0];

		string title = "Mission Completed";
		string message = "Mission: " + plan.m_currentMission.m_name;

		if (plan.m_result == MissionPlan.Result.Success) {

			message += " is a success!";

			List<Site.AssetSlot> hiddenAssets = new List<Site.AssetSlot> ();

			foreach (Site.AssetSlot aSlot in plan.m_missionSite.assets) {

				if (aSlot.m_state == Site.AssetSlot.State.Hidden) {

					hiddenAssets.Add (aSlot);
				}
			}

			if (hiddenAssets.Count > 0) {

				int rand = Random.Range (0, hiddenAssets.Count);

				Site.AssetSlot aSlot = hiddenAssets [rand];
				aSlot.m_state = Site.AssetSlot.State.Revealed;

				message += "\n" + aSlot.m_asset.m_name + " is revealed in " + plan.m_missionSite.m_siteName + ".";

			} else {

				message += "\nThere are no hidden Assets to reveal";
			}

			// update site alert level

			plan.m_missionSite.UpdateAlert (1);

		} else if (plan.m_result == MissionPlan.Result.Fail) {

			message += " is a failure.";

		}

		player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message);

		foreach (Player.ActorSlot aSlot in plan.m_actorSlots) {

			if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {

				aSlot.m_actor.notifications.AddNotification(GameController.instance.GetTurnNumber(), title, message);
			}
		}
	}
}
