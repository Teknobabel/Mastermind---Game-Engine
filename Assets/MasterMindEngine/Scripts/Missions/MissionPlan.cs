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

	public List<Mission> m_missionOptions = new List<Mission>();

	public int m_maxActorSlots = 1;

	public Site.AssetSlot m_currentAsset;

	public Site m_missionSite;

	public Lair.FloorSlot m_floorSlot;

	public Player.ActorSlot m_targetActor;

	public Region m_targetRegion;

	public SiteTrait m_targetSiteTrait;

	public State m_state = State.Planning;

	public OmegaPlan.OPGoal m_goal;

	public int m_missionID = -1;

	public bool 
	m_new = true,
	m_allowRepeat = true,
	m_doRepeat = false;

	// info returned from compiling mission

	public List<Trait> m_requiredTraits = new List<Trait> ();
	public List<Trait> m_matchingTraits = new List<Trait> ();

	public List<Asset> m_requiredAssets = new List<Asset> ();
	public List<Site.AssetSlot> m_linkedPlayerAssets = new List<Site.AssetSlot> (); // keeps a ref to assets used so other missions don't use them

	public List<Floor> m_requiredFloors = new List<Floor>();
	public List<Lair.FloorSlot> m_matchingFloors = new List<Lair.FloorSlot>();

	public List<EffectPool.EffectSlot> m_effects = new List<EffectPool.EffectSlot>();

	public int m_successChance = -1;
	public int m_turnNumber = 0;

	// info returned from completing the mission

	public Result m_result = Result.None;
}
