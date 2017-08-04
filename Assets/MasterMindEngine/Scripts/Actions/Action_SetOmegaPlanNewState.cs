using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_SetOmegaPlanNewState : Action {

	public OmegaPlan.OPGoal m_goal;
	public bool m_newState = false;

	public override void ExecuteAction ()
	{
		m_goal.m_new = false;

		Player player = GameEngine.instance.game.playerList [0];
		GameController.instance.Notify (player, GameEvent.Player_OmegaPlanChanged);
	}
}
