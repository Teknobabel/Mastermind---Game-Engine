using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPlan {

	public enum State {
		Planning,
		Active,
		Complete,
	}

	public enum Result
	{
		None,
		Success,
		Fail,
	}

	public Mission m_currentMission;

	public List<Player.ActorSlot> m_actorSlots = new List<Player.ActorSlot>();

	public Site.AssetSlot m_currentAsset;

	public Site m_missionSite;

	public Lair.FloorSlot m_floorSlot;

	public State m_state = State.Planning;

	public bool m_new = true;

	// info returned from compiling mission

	public List<Trait> m_requiredTraits = new List<Trait> ();
	public List<Trait> m_matchingTraits = new List<Trait> ();

	public int m_successChance = -1;
	public int m_turnNumber = 0;

	// info returned from completing the mission

	public Result m_result = Result.None;
}
