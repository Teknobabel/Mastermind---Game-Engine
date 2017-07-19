using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: IBaseObject, ISubject {

	public class CommandPool
	{
		public int 
		m_basePool,
		m_currentPool;
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
		}
	}

	public class HenchmenPool {

		public List<ActorSlot> m_henchmenSlots = new List<ActorSlot>();
	}

	public class HiringPool
	{
		public List<Actor> m_availableHenchmen = new List<Actor>();
		public List<ActorSlot> m_hireSlots = new List<ActorSlot>();
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
	private OmegaPlan m_omegaPlan;

	// assets
	private List<Asset> m_assets = new List<Asset>();

	// notifications
	private NotificationCenter m_notifications = new NotificationCenter();

	private List<MissionPlan> m_currentMissions = new List<MissionPlan> ();

	public void SpendCommandPoints (int amt)
	{
		m_commandPool.m_currentPool = Mathf.Clamp (m_commandPool.m_currentPool - amt, 0, 99);
	}

	public void AddOmegaPlan (OmegaPlan newOP)
	{
		m_omegaPlan = newOP;
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
		m_assets.Add (newAsset);
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

	public void AddObserver (IObserver observer){}

	public void RemoveObserver (IObserver observer){}

	public void Notify (ISubject subject, GameEvent thisGameEvent){}

	//

	public OmegaPlan omegaPlan {get{return m_omegaPlan;}}

	public int id {get{ return m_id; } set{ m_id = value; }}

	public HiringPool hiringPool {get{return m_hiringPool;}}

	public HenchmenPool henchmenPool {get{return m_henchmenPool;}}

	public Lair lair {get{ return m_lair; }}

	public NotificationCenter notifications {get{ return m_notifications;}}

	public CommandPool commandPool {get{ return m_commandPool;}}

	public List<MissionPlan> currentMissions {get{ return m_currentMissions;}}
}
