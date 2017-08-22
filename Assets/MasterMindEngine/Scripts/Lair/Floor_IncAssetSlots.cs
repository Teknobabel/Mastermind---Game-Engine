using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_IncAssetSlots : Floor {

	public int m_numSlots = 5;

	public override void Initialize ()
	{
		base.Initialize ();

		Player player = GameEngine.instance.game.playerList [0];

		player.baseAssetSlots += m_numSlots;

//		GameController.instance.Notify (player, GameEvent.Player_HenchmenPoolChanged);
	}
}
