using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_IncCommandPool : Floor {

	public int m_bonus = 2;

	public override void Initialize ()
	{
		base.Initialize ();

		Player player = GameEngine.instance.game.playerList [0];

		Action_UpdateBaseCommandPool updateCP = new Action_UpdateBaseCommandPool ();
		updateCP.m_amount = m_bonus;
		updateCP.m_playerID = 0;
		GameController.instance.ProcessAction (updateCP);

		GameController.instance.Notify (player, GameEvent.Player_CommandPoolChanged);
	}
}
