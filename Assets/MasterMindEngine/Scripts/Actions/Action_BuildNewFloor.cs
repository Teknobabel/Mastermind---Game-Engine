using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_BuildNewFloor : Action {

	public Player m_player = null;

	public Floor m_floor = null;

	public override void ExecuteAction ()
	{
		Debug.Log ("Player building floor: " + m_floor.m_name);

		string title = "New Floor Built";
		string message = m_floor.m_name + " has been added to your Lair.";

		m_player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Lair, false, m_missionID);

		Floor newFloor = (Floor)Object.Instantiate (m_floor);
		m_player.lair.AddFloor (newFloor);

		// notify UI for updating

		GameController.instance.Notify (m_player, GameEvent.Player_LairChanged);
	}
}
