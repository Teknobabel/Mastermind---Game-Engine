using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_SpendCommandPoints : Action {

	public int m_amount = 0;

	public int m_playerID = -1;

	public override void ExecuteAction ()
	{
		Debug.Log ("Spending " + m_amount + " Command Points.");

		Player player = GameEngine.instance.game.playerList [m_playerID];

		player.commandPool.m_currentPool = Mathf.Clamp (player.commandPool.m_currentPool - m_amount, 0, 99);

		GameController.instance.Notify (player, GameEvent.Player_CommandPoolChanged);
	}
}
