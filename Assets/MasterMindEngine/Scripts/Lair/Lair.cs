using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lair {

	public class FloorSlot
	{
		public enum FloorState
		{
			Empty,
			Idle,
			Planning,
			MissionInProgress,
		}

		public Floor m_floor;
		public FloorState m_state;
		public int m_id = -1;
		public List<Player.ActorSlot> m_actorSlots = new List<Player.ActorSlot> ();
		public MissionPlan m_missionPlan = new MissionPlan();
		public bool m_new = true;
	}

	private List<FloorSlot> m_floorSlots = new List<FloorSlot>();

	private int m_floorID = 0;

	public void AddFloor (Floor newFloor)
	{
		FloorSlot newFloorSlot = new FloorSlot ();
		newFloorSlot.m_id = m_floorID;
		m_floorID++;
		newFloorSlot.m_floor = newFloor;
		newFloorSlot.m_state = FloorSlot.FloorState.Idle;
		newFloorSlot.m_missionPlan.m_floorSlot = newFloorSlot;

		for (int i = 0; i < newFloor.m_startingHenchmenSlots; i++) {
		
			Player.ActorSlot aSlot = new Player.ActorSlot ();
			aSlot.m_state = Player.ActorSlot.ActorSlotState.Empty;
			aSlot.m_new = false;
			newFloorSlot.m_actorSlots.Add (aSlot);
		}

		m_floorSlots.Add (newFloorSlot);

		newFloor.Initialize ();

	}

	public List<FloorSlot> floorSlots {get{ return m_floorSlots;}}
}
