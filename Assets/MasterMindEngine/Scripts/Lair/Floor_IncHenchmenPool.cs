using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_IncHenchmenPool : Floor {

	public int m_numSlots = 1;

	public override void Initialize ()
	{
		base.Initialize ();

		Player player = GameEngine.instance.game.playerList [0];

		for (int i = 0; i < m_numSlots; i++) {
			
			Player.ActorSlot newSlot = new Player.ActorSlot ();
			newSlot.m_state = Player.ActorSlot.ActorSlotState.Empty;
			player.henchmenPool.m_henchmenSlots.Add (newSlot);

		}

		GameController.instance.Notify (player, GameEvent.Player_HenchmenPoolChanged);
	}
}
