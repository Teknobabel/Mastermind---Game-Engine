using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_GainInfamy : Action {

	public int m_playerID = -1;
	public int m_amount = 0;

	public override void ExecuteAction ()
	{
		Player player = GameEngine.instance.game.playerList [m_playerID];
		player.GainInfamy (m_amount);

		string title = "Infamy Increased";
		string message = "Your organization has gained " + m_amount.ToString() + " Infamy.";
		player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message);
	}
}
