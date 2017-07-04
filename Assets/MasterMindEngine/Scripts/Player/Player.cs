using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: IBaseObject {

	public class CommandPool
	{
		public int 
		m_basePool,
		m_currentPool;
	}

	public struct ActorSlot
	{
		public enum ActorSlotState
		{
			Empty,
			Active,
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

	// command pool
	private CommandPool m_commandPool;

	// omega plan
	private OmegaPlan m_omegaPlan;

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

	public OmegaPlan omegaPlan {get{return m_omegaPlan;}}

	public int id {get{ return m_id; } set{ m_id = value; }}
}
