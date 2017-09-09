using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_CombineAssets : Mission {

//	public Asset[] m_requiredAssets;

	public Asset m_gainedAsset;

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

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

				Player player = GameEngine.instance.game.playerList [0];

				Action_GainAsset gainAsset = new Action_GainAsset ();
				gainAsset.m_asset = m_gainedAsset;
				gainAsset.m_player = player;
				GameController.instance.ProcessAction (gainAsset);
			} else {

				// player doesn't have required assets

				Player player = GameEngine.instance.game.playerList [0];

				string title = "Warning";
				string message = "You don't own all the required Assets for this Mission.";
				player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Assets,false, plan.m_missionID);

			}
		}
	}
}
