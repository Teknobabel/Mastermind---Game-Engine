using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_ChangeAlertLevel : Action {

	public int m_playerID = -1;
	public int m_siteID = -1;
	public int m_amount = 0;

	public override void ExecuteAction ()
	{
		Player player = GameEngine.instance.game.playerList [m_playerID];

		Site site = GameController.instance.GetSite (m_siteID);
//		site.UpdateAlert(m_amount);
		site.alertLevelChange += m_amount;

		string title = "Alert Level Changed";
		string message = "The Alert Level in " + site.m_siteName + " has ";

		if (m_amount > 0) {

			message += "increased by " + m_amount.ToString () + ".";
		} else if (m_amount < 0) {
			message += "decreased by " + m_amount.ToString () + ".";
		}

		player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.World, false, m_missionID);
	}

}
