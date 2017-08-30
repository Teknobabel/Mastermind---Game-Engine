using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_GainTrait : Action {

	public int m_playerID = -1;

	public Trait m_newTrait;

	public Actor m_actor;

	public override void ExecuteAction ()
	{
		m_actor.AddTrait (m_newTrait);

		string title = "New Trait Acquired";
		string message = m_actor.m_actorName + " gains the " + m_newTrait.m_name + " Trait.";
		Player player = GameEngine.instance.game.playerList [m_playerID];
		player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Contacts);
		m_actor.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Contacts);
	}
}
