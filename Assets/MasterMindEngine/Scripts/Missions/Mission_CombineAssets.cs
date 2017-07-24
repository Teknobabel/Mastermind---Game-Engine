using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_CombineAssets : Mission {

	public Asset[] m_requiredAssets;

	public Asset m_gainedAsset;

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

		if (plan.m_result == MissionPlan.Result.Success) {
			
			List<Site.AssetSlot> assets = GameController.instance.GetAssets (0);
			List<Asset> baseList = new List<Asset> ();
			List<Asset> foundAssets = new List<Asset> ();

			foreach (Site.AssetSlot aSlot in assets) {

				baseList.Add (aSlot.m_asset);
			}

			foreach (Asset a in m_requiredAssets) {

				if (baseList.Contains (a)) {

					baseList.Remove (a);
					foundAssets.Add (a);
				}
			}

			if (foundAssets.Count == m_requiredAssets.Length) {

				// remove consumed assets

				foreach (Asset a in foundAssets) {
					Action_RemoveAsset removeAsset = new Action_RemoveAsset ();
					removeAsset.m_playerID = 0;
					removeAsset.m_asset = a;
					GameController.instance.ProcessAction (removeAsset);
				}

				// grant new asset

				Action_GainAsset gainAsset = new Action_GainAsset ();
				gainAsset.m_asset = m_gainedAsset;
				gainAsset.m_player = player;
				GameController.instance.ProcessAction (gainAsset);
			} else {

				// player doesn't have required assets
				title = "Warning";
				message = "You don't own all the required Assets for this Mission.";
				player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message);

			}
		}
	}
}
