using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_RemoveSiteTrait : Action {

		public int m_siteID = -1;
		public SiteTrait m_trait;

	public override void ExecuteAction ()
	{
		Site site = GameController.instance.GetSite (m_siteID);
		site.RemoveTrait (m_trait);

		string title = "Trait Lost";
		string message = site.m_siteName + " lost the " + m_trait.m_name + " Trait";
		Player player = GameEngine.instance.game.playerList [0];
		player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message);
	}
}
