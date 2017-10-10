using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Director : ScriptableObject {

	[System.Serializable]
	public class AlertData {
		
		public Site.Type m_siteType = Site.Type.None;
		public Trait[] m_traitList;
	}

	[System.Serializable]
	public class AssetBank {

		public Asset[] m_assets;
	}

	public Actor[] m_startingHenchmen;

	public Actor[] m_startingHireableHenchmen;

	public OmegaPlan m_startingOmegaPlan;

	public Asset[] m_startingAssets;

	public Lair m_startingLair;

	public AlertData[] m_alertData;

	public AssetBank[] m_assetBank;

	public int 
	m_startingHireSlots = 3,
	m_startingHenchmenSlots = 3,
	m_startingCommandPool = 10,
	m_startingAssetSlots = 5;
//	m_startingFloorSlots = 6;
}
