using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_UpdateBaseCommandPool : Action {

	public int m_playerID = -1;
	public int m_amount = 0;

	public override void ExecuteAction ()
	{
		Player player = GameEngine.instance.game.playerList [m_playerID];

		player.commandPool.UpdateBaseCommandPool (m_amount);

		GameController.instance.Notify (player, GameEvent.Player_CommandPoolChanged);

		string title = "Command Pool Increase";
		string message = "Your maximum Command Pool has increased by " + m_amount.ToString() + ".";

		player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message);
	}
}
