using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_RefillCommandPool : Action {

	public int m_playerID = -1;

	public override void ExecuteAction ()
	{
		Player player = GameEngine.instance.game.playerList [m_playerID];

		player.commandPool.UpdateCommandPool (player.commandPool.m_income);

		GameController.instance.Notify (player, GameEvent.Player_CommandPoolChanged);
	}
}
