using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_SetMissionNewState : Action {

	public MissionPlan m_plan;
	public bool m_newState = false;

	public override void ExecuteAction ()
	{
		m_plan.m_new = m_newState;

		Player player = GameEngine.instance.game.playerList [0];
		GameController.instance.Notify (player, GameEvent.Player_NewMissionStarted);
	}
}
