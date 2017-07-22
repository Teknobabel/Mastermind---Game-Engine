using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_SetFloorNewState : Action {

	public Lair.FloorSlot m_floorSlot;
	public bool m_newState = false;

	public override void ExecuteAction ()
	{
		m_floorSlot.m_new = m_newState;

		Player player = GameEngine.instance.game.playerList [0];

		GameController.instance.Notify (player, GameEvent.Player_LairChanged);
	}
}
