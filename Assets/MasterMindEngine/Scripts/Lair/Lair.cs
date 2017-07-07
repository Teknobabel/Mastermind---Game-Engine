using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lair {

	public struct FloorSlot
	{
		public enum FloorState
		{
			Empty,
			Idle,
			MissionInProgress,
		}

		public Floor m_floor;
		public FloorState m_state;
	}

	private List<FloorSlot> m_floorSlots = new List<FloorSlot>();

	public void AddFloor (Floor newFloor)
	{
		FloorSlot newFloorSlot = new FloorSlot ();
		newFloorSlot.m_floor = newFloor;
		newFloorSlot.m_state = FloorSlot.FloorState.Idle;
		m_floorSlots.Add (newFloorSlot);

	}
}
