using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_BuildNewFloor : Action {

	public Player m_player = null;

	public Floor m_floor = null;

	public override void ExecuteAction ()
	{
		Debug.Log ("Player building floor: " + m_floor.m_name);

		m_player.lair.AddFloor (m_floor);
	}
}
