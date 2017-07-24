using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_RemoveAsset : Action {

	public int m_playerID;
	public Asset m_asset = null;
	public Site.AssetSlot m_assetSlot = null;

	public override void ExecuteAction ()
	{
		if (GameEngine.instance.game.playerList.ContainsKey (m_playerID)) {

			Player player = GameEngine.instance.game.playerList [m_playerID];

			string title = "Asset Removed";
			string message = "";

			if (m_asset != null) {

				player.RemoveAsset (m_asset);

				message = m_asset.m_name + " has been removed.";

			} else if (m_assetSlot != null) {

				player.RemoveAsset (m_assetSlot);

				message = m_assetSlot.m_asset.m_name + " has been removed.";
			}

			player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message);

		} else {
			Debug.Log ("Player ID not found");
		}
	}
}
