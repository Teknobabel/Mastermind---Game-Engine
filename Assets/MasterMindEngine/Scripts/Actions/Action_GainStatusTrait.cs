using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_GainStatusTrait : Action {

	public int m_playerID = -1;

	public StatusTrait m_newStatus;

	public Actor m_actor;

	public override void ExecuteAction ()
	{
		m_actor.m_status = m_newStatus;

		string title = "Status Updated";
		string message = m_actor.m_actorName + "'s Status is now " + m_newStatus.m_name + ".";

		Player player = null;

		if (GameEngine.instance.game.playerList.ContainsKey (m_playerID)) {

			player = GameEngine.instance.game.playerList [m_playerID];
		} else if (GameEngine.instance.game.agentPlayerList.ContainsKey (m_playerID)) {

			player = GameEngine.instance.game.agentPlayerList [m_playerID];
		} 

		if (player != null) {
			
			player.notifications.AddNotification (GameController.instance.GetTurnNumber (), title, message, EventLocation.Contacts, false, m_missionID);
		}
		m_actor.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Contacts, false, m_missionID);
	}
}
