using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_ChangeAgentVisibilityState : Action {

	public Player.ActorSlot m_agentSlot;
	public Player.ActorSlot.VisibilityState m_newVisibilityState;

	public override void ExecuteAction ()
	{
		if (m_agentSlot.m_visibilityState == Player.ActorSlot.VisibilityState.Hidden && m_newVisibilityState == Player.ActorSlot.VisibilityState.Visible) {
		
			// alert player agent has been revealed
			string title = "Agent Revealed";
			string message = "Agent " + m_agentSlot.m_actor.m_actorName + " has been revealed in " + m_agentSlot.m_currentSite.m_siteName + "!";
			Player player = GameController.instance.game.playerList [0];
			player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.World, false, m_missionID);
		}

		m_agentSlot.m_visibilityState = m_newVisibilityState;
	}
}
