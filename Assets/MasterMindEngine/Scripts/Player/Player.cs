using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: IBaseObject, ISubject, IEffectable, IAffinity {

	public class CommandPool
	{
		public int 
		m_basePool,
		m_currentPool,
		m_income;

		public void UpdateBaseCommandPool (int amount)
		{
			m_basePool += amount;
		}

		public void UpdateCommandPool (int amount)
		{
			m_currentPool = Mathf.Clamp (m_currentPool + amount, 0, m_basePool);
		}

		public void UpdateIncome (int amount)
		{
			m_income += amount;
		}
	}

	public class IntelSlot {

		public enum IntelState
		{
			None,
			Owned,
			Contested,
			Stolen,
		}

		public IntelState m_intelState = IntelState.None;
	}

	public class ActorSlot
	{
		public enum ActorSlotState
		{
			Empty,
			Active,
			OnMission,
		}

		public enum VisibilityState
		{
			Visible,
			Hidden,
		}

		public Actor m_actor;
		public bool m_new;
		public ActorSlotState m_state;
		public VisibilityState m_visibilityState;
		public int m_turnsPresent = 0;
		public Site m_currentSite = null;

		public void SetHenchmen (Actor a)
		{
			if (m_state != ActorSlotState.Active) {

				m_actor = a;
				m_new = true;
				m_state = ActorSlotState.Active;
			}
		}

		public void RemoveHenchmen ()
		{
			m_actor = null;
			m_new = false;
			m_state = ActorSlotState.Empty;
			m_turnsPresent = 0;
		}
	}

	public class OmegaPlanSlot 
	{
		public enum State
		{
			None,
			New,
		}

		public OmegaPlan m_omegaPlan;
		public State m_state = State.None;
	}

	public class HenchmenPool {

		public List<ActorSlot> m_henchmenSlots = new List<ActorSlot>();
	}

	public class HiringPool
	{
		public List<Actor> m_availableHenchmen = new List<Actor>();
		public List<Actor> m_tempBank = new List<Actor>(); // makes sure any actors fired / left don't immediate get added back in
		public List<ActorSlot> m_hireSlots = new List<ActorSlot>();
		public List<Trait> m_seekingSkills = new List<Trait> ();

		public Actor GetHenchmenToHire (int infamy)
		{
			Actor actor = null;

			int rank2Threshold = 20;
			int rank3Threshold = 50;
			int rank4Threshold = 10;

			int maxRank = 1;

			if (infamy >= rank4Threshold) {

				maxRank = 4;
			} else if (infamy >= rank3Threshold) {
				maxRank = 3;
			}
			else if (infamy >= rank2Threshold) {
				maxRank = 2;
			}

			List<Actor> validHenchmen = new List<Actor> ();

			foreach (Actor a in m_availableHenchmen) {

				if (a.m_rank <= maxRank) {

					validHenchmen.Add (a);

					foreach (Trait t in m_seekingSkills) {
						
						if (a.traits.Contains (t)) {

							validHenchmen.Add (a);
							validHenchmen.Add (a);
							validHenchmen.Add (a);
							break;
						}
					}
				}
			}

			if (validHenchmen.Count > 0) {

				int rand = Random.Range (0, validHenchmen.Count);
				actor = (Actor)validHenchmen [rand];
			}

			return actor;
		}
	}

	private int m_id = -1;

	private Lair m_lair;

	protected HiringPool m_hiringPool;

	// current henchmen
	protected HenchmenPool m_henchmenPool;

	private CommandPool m_commandPool;

	private OmegaPlanSlot m_omegaPlanSlot;

	private List<Site.AssetSlot> m_assets = new List<Site.AssetSlot>();

	private NotificationCenter m_notifications = new NotificationCenter();

	private List<MissionPlan> m_currentMissions = new List<MissionPlan> ();

	private List<MissionSummary> m_missionsCompletedThisTurn = new List<MissionSummary> ();

	private MessageCenter m_messageCenter = new MessageCenter();

	private EffectPool m_effectPool = new EffectPool();

	private Dictionary<int, AffinitySlot> m_affinityList = new Dictionary<int, AffinitySlot>();

	private int m_infamy = 0,
	m_baseAssetSlots = 0,
	m_numFloorSlots = 0;

	private List<IntelSlot> m_intel = new List<IntelSlot> ();

	public virtual void Initialize ()
	{

	}

	public void SpendCommandPoints (int amt)
	{
		m_commandPool.m_currentPool = Mathf.Clamp (m_commandPool.m_currentPool - amt, 0, 99);
	}

	public void AddOmegaPlan (OmegaPlan newOP)
	{
		OmegaPlanSlot opSlot = new OmegaPlanSlot ();
		opSlot.m_omegaPlan = newOP;
		opSlot.m_state = OmegaPlanSlot.State.New;

		m_omegaPlanSlot = opSlot;
	}

	public void AddCommandPool (CommandPool cp)
	{
		m_commandPool = cp;
	}

	public void AddLair (Lair newLair)
	{
		m_lair = newLair;
	}

	public void AddHiringPool (HiringPool hiringPool)
	{
		m_hiringPool = hiringPool;
	}

	public void AddHenchmenPool (HenchmenPool henchmenPool)
	{
		m_henchmenPool = henchmenPool;
	}

	public void AddAsset (Asset newAsset)
	{
		// check for Intel recovery

		if (newAsset.m_name == "Intel") {

			foreach (IntelSlot iSlot in m_intel) {

				if (iSlot.m_intelState == IntelSlot.IntelState.Contested) {

					iSlot.m_intelState = IntelSlot.IntelState.Owned;
					break;
				}
			}

			string title = "Intel Recovered";
			string message = "Your Henchmen have recovered the Intel.";

			m_notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Missions, false, -1);

			GameController.instance.Notify (this, GameEvent.Player_IntelChanged);

		} else {

			Site.AssetSlot aSlot = new Site.AssetSlot ();
			aSlot.m_asset = newAsset;
			aSlot.m_state = Site.AssetSlot.State.Revealed;
			aSlot.m_new = true;
			m_assets.Add (aSlot);
		}

	}

	public bool HasAsset (Asset asset)
	{

		foreach (Site.AssetSlot aSlot in m_assets) {

			if (aSlot.m_asset.m_name == asset.m_name) {
				return true;
			}
		}

		return false;
	}

	public void RemoveAsset (Asset oldAsset)
	{
		for (int i=0; i < m_assets.Count; i++) {

			Site.AssetSlot aSlot = m_assets [i];

			if (aSlot.m_asset == oldAsset) {

				aSlot.m_asset = null;
				m_assets.RemoveAt (i);
				return;
			}
		}
	}

	public void RemoveAsset (Site.AssetSlot oldSlot)
	{
		for (int i=0; i < m_assets.Count; i++) {

			Site.AssetSlot aSlot = m_assets [i];

			if (aSlot == oldSlot) {

				aSlot.m_asset = null;
				m_assets.RemoveAt (i);
				return;
			}
		}
	}

	public int NumAssetSlots ()
	{
		int numSlots = GameEngine.instance.game.director.m_startingAssetSlots + m_baseAssetSlots;
		return numSlots;
	}

	public int NumFloorSlots ()
	{
		int numSlots = m_lair.maxFloors + m_numFloorSlots;
		return numSlots;
	}

	public void AddMission (MissionPlan plan)
	{
		m_currentMissions.Add (plan);
	}

	public void RemoveMission (MissionPlan plan)
	{
		if (m_currentMissions.Contains (plan)) {
			m_currentMissions.Remove (plan);
		}
	}

	public void GainInfamy (int amount)
	{
		m_infamy += amount;
	}

	public int GetAffinityScore (int targetID, IAffinity target)
	{
		if (m_affinityList.ContainsKey (targetID)) {

			AffinitySlot aSlot = m_affinityList [targetID];
			return aSlot.m_affinityScore;

		} else {

			AffinitySlot aSlot = new AffinitySlot ();
			aSlot.m_affinityReference = target;
			m_affinityList.Add (targetID, aSlot);
			return aSlot.m_affinityScore;

		}
	}

	public void UpdateAffinity (int targetID, int amount, IAffinity target)
	{
		if (m_affinityList.ContainsKey (targetID)) {

			AffinitySlot aSlot = m_affinityList [targetID];
			aSlot.m_affinityScore = Mathf.Clamp (aSlot.m_affinityScore + amount, -100, 100);

		} else {

			AffinitySlot aSlot = new AffinitySlot ();
			aSlot.m_affinityReference = target;
			m_affinityList.Add (targetID, aSlot);
			aSlot.m_affinityScore = Mathf.Clamp (aSlot.m_affinityScore + amount, -100, 100);

		}
	}

	public void AddObserver (IObserver observer){}

	public void RemoveObserver (IObserver observer){}

	public void Notify (ISubject subject, GameEvent thisGameEvent){}

	//

	public OmegaPlanSlot omegaPlanSlot {get{return m_omegaPlanSlot;}}

	public int id {get{ return m_id; } set{ m_id = value; }}

	public HiringPool hiringPool {get{return m_hiringPool;}}

	public HenchmenPool henchmenPool {get{return m_henchmenPool;}}

	public Lair lair {get{ return m_lair; }}

	public NotificationCenter notifications {get{ return m_notifications;}}

	public CommandPool commandPool {get{ return m_commandPool;}}

	public List<MissionPlan> currentMissions {get{ return m_currentMissions;}}

	public List<Site.AssetSlot> assets {get{ return m_assets; }}

	public MessageCenter messageCenter {get{ return m_messageCenter; }}

	public int infamy {get{return m_infamy;}}

	public int baseAssetSlots {get{ return m_baseAssetSlots; } set{ m_baseAssetSlots = value; }}

	public int numFloorSlots {get{ return m_numFloorSlots; }set{ m_numFloorSlots = value; }}

	public List<IntelSlot> intel {get{ return m_intel; } set{ m_intel = value; }}

	public List<MissionSummary> missionsCompletedThisTurn {get{return m_missionsCompletedThisTurn; }set{m_missionsCompletedThisTurn = value;}}

	public EffectPool effectPool {get{ return m_effectPool; }}

	public Dictionary<int, AffinitySlot> affinityList { get { return m_affinityList; }}
}
