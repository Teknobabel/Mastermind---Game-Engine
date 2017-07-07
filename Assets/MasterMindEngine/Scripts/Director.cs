using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Director : ScriptableObject {

	public Actor[] m_startingHenchmen;

	public OmegaPlan m_startingOmegaPlan;

	public Asset[] m_startingAssets;

	public Floor[] m_startingFloors;

	public int 
	m_startingHireSlots = 3,
	m_startingHenchmenSlots = 3,
	m_startingCommandPool = 10;
}
