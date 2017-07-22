using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_SetOmegaPlanNewState : Action {

	public Player.OmegaPlanSlot m_omegaPlanSlot;
	public bool m_newState = false;

	public override void ExecuteAction ()
	{
		m_omegaPlanSlot.m_state = Player.OmegaPlanSlot.State.None;

		Player player = GameEngine.instance.game.playerList [0];
		GameController.instance.Notify (player, GameEvent.Player_OmegaPlanChanged);
	}
}
