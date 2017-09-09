using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_RevealAsset : Action {

	public Site.AssetSlot m_assetSlot;
	public int m_playerID = -1;

	public override void ExecuteAction ()
	{
		m_assetSlot.m_state = Site.AssetSlot.State.Revealed;
		Player player = GameEngine.instance.game.playerList [m_playerID];

		string title = "Asset Revealed";
		string message = m_assetSlot.m_asset.m_name + " is revealed in " + m_assetSlot.m_site.m_siteName + ".";
		player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.World, false, m_missionID);
	}
}
