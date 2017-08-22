using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_SetActorNewState : Action {

	public Player.ActorSlot m_actorSlot;
	public bool m_newState = false;

	public override void ExecuteAction ()
	{
		m_actorSlot.m_new = m_newState;

		Player player = GameEngine.instance.game.playerList [0];
		GameController.instance.Notify (player, GameEvent.Henchmen_NewStateChanged);
	}
}
