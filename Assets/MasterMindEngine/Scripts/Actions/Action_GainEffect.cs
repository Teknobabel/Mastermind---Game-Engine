using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_GainEffect : Action {

	public int m_playerID = -1;
	public Effect m_effect = null;
	public IEffectable m_effectTarget = null;

	public override void ExecuteAction ()
	{
		Debug.Log ("Player gaining effect: " + m_effect.m_effectName);
		Player player = GameEngine.instance.game.playerList [m_playerID];

		m_effectTarget.effectPool.AddEffect (m_effect);

		string title = "New Effect Added";
		string message = "Effect: " + m_effect.m_effectName + " gained for " + m_effect.m_duration.ToString() + " Turns.";

		player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.None, false, m_missionID);

	}
}
