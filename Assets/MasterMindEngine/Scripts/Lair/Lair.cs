using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Lair : ScriptableObject {

	public Floor[] m_startingFloors;
	public Floor m_builderFloor;
	public int m_maxStartingFloors = 6;

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
		public int m_numActorSlots = 1;
		public List<Player.ActorSlot> m_actorSlots = new List<Player.ActorSlot> ();
		public MissionPlan m_missionPlan = new MissionPlan();
		public bool m_new = true;
	}

	private List<FloorSlot> m_floorSlots = new List<FloorSlot>();
	private List<Mission> m_unlockedFacilities = new List<Mission>();
	private Lair.FloorSlot m_builder;

	private int 
	m_floorID = 0,
	m_maxFloors = 10;

	public void Initialize ()
	{
		m_maxFloors = m_maxStartingFloors;

		foreach (Floor f in m_startingFloors) {

			Action_BuildNewFloor buildFloor = new Action_BuildNewFloor ();
			buildFloor.m_player = GameController.instance.game.playerList [0];
			buildFloor.m_floor = f;
			GameController.instance.ProcessAction (buildFloor);
		}

		Action_BuildNewFloor builderFloor = new Action_BuildNewFloor ();
		builderFloor.m_player = GameController.instance.game.playerList [0];
		builderFloor.m_floor = m_builderFloor;
		GameController.instance.ProcessAction (builderFloor);
	}

	public void AddFloor (Floor newFloor)
	{
		FloorSlot newFloorSlot = new FloorSlot ();
		newFloorSlot.m_id = m_floorID;
		m_floorID++;
		newFloorSlot.m_numActorSlots = newFloor.m_startingHenchmenSlots;
		newFloorSlot.m_floor = newFloor;
		newFloorSlot.m_state = FloorSlot.FloorState.Idle;
		newFloorSlot.m_missionPlan.m_floorSlot = newFloorSlot;

//		for (int i = 0; i < newFloor.m_startingHenchmenSlots; i++) {
//		
//			Player.ActorSlot aSlot = new Player.ActorSlot ();
//			aSlot.m_state = Player.ActorSlot.ActorSlotState.Empty;
//			aSlot.m_new = false;
//			newFloorSlot.m_actorSlots.Add (aSlot);
//		}

		if (newFloor.m_name == "Builder") {

			m_builder = newFloorSlot;

		} else {
			
			m_floorSlots.Add (newFloorSlot);
		}

		newFloor.Initialize ();

	}

	public List<FloorSlot> floorSlots {get{ return m_floorSlots;}}
	public int maxFloors {get{ return m_maxFloors; }set{ m_maxFloors = value; }}
	public List<Mission> unlockedFacilities {get{ return m_unlockedFacilities; } set{ m_unlockedFacilities = value; }}
	public Lair.FloorSlot builder {get{return m_builder;}}
}
