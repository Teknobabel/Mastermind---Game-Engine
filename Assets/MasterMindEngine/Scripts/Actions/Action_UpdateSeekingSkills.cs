using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_UpdateSeekingSkills : Action {

	public int m_playerID = -1;

	public List<Trait> m_seekingSkills = new List<Trait> ();

	public override void ExecuteAction ()
	{
		Player player = GameEngine.instance.game.playerList [m_playerID];
		player.hiringPool.m_seekingSkills = m_seekingSkills;
	}
}
