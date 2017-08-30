using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: IBaseObject, ISubject {

	public class CommandPool
	{
		public int 
		m_basePool,
		m_currentPool;

		public void UpdateBaseCommandPool (int amount)
		{
			m_basePool += amount;
		}
	}

	public class ActorSlot
	{
		public enum ActorSlotState
		{
			Empty,
			Active,
			OnMission,
		}
		public Actor m_actor;
		public bool m_new;
		public ActorSlotState m_state;
		public int m_turnsPresent = 0;

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

	// lair

	private Lair m_lair;

	// hiring pool
	private HiringPool m_hiringPool;

	// current henchmen
	private HenchmenPool m_henchmenPool;

	// command pool
	private CommandPool m_commandPool;

	// omega plan
	private OmegaPlanSlot m_omegaPlanSlot;

	// assets
	private List<Site.AssetSlot> m_assets = new List<Site.AssetSlot>();

	// notifications
	private NotificationCenter m_notifications = new NotificationCenter();

	private List<MissionPlan> m_currentMissions = new List<MissionPlan> ();

	private MessageCenter m_messageCenter = new MessageCenter();

	private int m_infamy = 0,
	m_baseAssetSlots = 0;

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
		Site.AssetSlot aSlot = new Site.AssetSlot ();
		aSlot.m_asset = newAsset;
		aSlot.m_state = Site.AssetSlot.State.Hidden;
		aSlot.m_new = true;
		m_assets.Add (aSlot);

		//		m_assets.Add (newAsset);
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

}
