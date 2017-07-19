using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPlan {

	public enum State {
		Planning,
		Active,
		Complete,
	}

	public Mission m_currentMission;

	public List<Player.ActorSlot> m_actorSlots = new List<Player.ActorSlot>();

	public Asset m_currentAsset;

	public Site m_missionSite;

	public Lair.FloorSlot m_floorSlot;

	public State m_state = State.Planning;

	// info returned from compiling mission

	public List<Trait> m_requiredTraits = new List<Trait> ();
	public List<Trait> m_matchingTraits = new List<Trait> ();

	public int m_successChance = -1;
	public int m_turnNumber = 0;
}
