using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Site : ScriptableObject, IBaseObject, IAffinity, IEffectable {

	public enum Type {
		None,
		Politics,
		Military,
		Economy,
	}

	public class AssetSlot
	{
		public enum State
		{
			None,
			Hidden,
			Revealed,
			InUse,
		}

		public Asset m_asset;
		public State m_state;
		public Site m_site;
		public bool m_new = false;
	}

	[System.Serializable]
	public struct RandomAssetList
	{
		public List<Asset> m_assets;
	}

	[System.Serializable]
	public struct RandomTraitList
	{
		public List<SiteTrait> m_traits;
	}

	public string m_siteName = "Site";

	public Type m_type = Type.None;

	public int m_maxAlertLevel = 5;

	public SiteTrait[] m_startingTraits;

	public List<RandomTraitList> m_randomStartingTraits = new List<RandomTraitList>();

	private List<AssetSlot> m_assets = new List<AssetSlot>();

	private List<SiteTrait> m_traits = new List<SiteTrait>();

	private List<Player.ActorSlot> m_agents = new List<Player.ActorSlot>();

	private int m_id = -1;

	private int m_currentAlertLevel = 0;

	private int m_regionID = -1;

	private int m_alertLevelChange = 0; // how much the alert level will change at the end of the turn

	private Dictionary<int, AffinitySlot> m_affinityList = new Dictionary<int, AffinitySlot>();

	private EffectPool m_effectPool = new EffectPool();

	public void Initialize ()
	{
//		foreach (Asset a in m_startingAssets) {
//
//			AddAsset (a);
//		}
//
//		foreach (RandomAssetList ral in m_randomStartingAssets) {
//
//			if (ral.m_assets.Count > 0) {
//
//				int rand = Random.Range (0, ral.m_assets.Count);
//				Asset a = ral.m_assets [rand];
//
//				if (a.m_name != "Blank") {
//					AddAsset (a);
//				}
//			}
//		}

		foreach (SiteTrait t in m_startingTraits) {

			AddTrait (t);
		}

		foreach (RandomTraitList rtl in m_randomStartingTraits) {

			if (rtl.m_traits.Count > 0) {

				int rand = Random.Range (0, rtl.m_traits.Count);
				SiteTrait t = rtl.m_traits [rand];

				if (!m_traits.Contains (t) && t.m_name != "Blank") {
					
					AddTrait (t);
				}
			}
		}

//		SiteTrait_LowerMissionSuccess newTrait = new SiteTrait_LowerMissionSuccess ();
//		newTrait.m_name = "Test Trait";
//		newTrait.m_type = Trait.Type.Site;
//		AddTrait (newTrait);
	}

	public void AddTrait (SiteTrait newTrait)
	{
//		Debug.Log ("Site " + m_id + " Adds " + newTrait.m_name);
		m_traits.Add (newTrait);
		newTrait.TraitAdded (this);
	}

	public void RemoveTrait (SiteTrait oldTrait)
	{
		if (m_traits.Contains (oldTrait)) {

			m_traits.Remove (oldTrait);
			oldTrait.TraitRemoved (this);
		}
	}

	public AssetSlot AddAsset (Asset newAsset)
	{
		AssetSlot aSlot = new AssetSlot ();
		aSlot.m_asset = newAsset;
		aSlot.m_state = AssetSlot.State.Hidden;
		aSlot.m_site = this;
		m_assets.Add (aSlot);

		return aSlot;
	}

	public void RemoveAsset (AssetSlot aSlot)
	{
		for (int i = 0; i < m_assets.Count; i++) {

			AssetSlot a = m_assets [i];

			if (aSlot == a) {

				m_assets.RemoveAt (i);
				aSlot.m_asset = null;
				return;
			}
		}
	}

	public void AddAgent (Player.ActorSlot agentSlot)
	{
		m_agents.Add (agentSlot);
		agentSlot.m_currentSite = this;
	}

	public void RemoveAgent (Player.ActorSlot agentSlot)
	{
		if (m_agents.Contains (agentSlot)) {

			m_agents.Remove (agentSlot);
			agentSlot.m_currentSite = null;
		}
	}

	public void UpdateAlert (int amount)
	{
		m_currentAlertLevel = Mathf.Clamp (m_currentAlertLevel + amount, 0, m_maxAlertLevel);
	}

	public void EndTurn ()
	{
		UpdateAlert (m_alertLevelChange);
		m_alertLevelChange = 0;
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

	public int id {get{ return m_id; } set{ m_id = value; }}

	public int currentAlertLevel {get{ return m_currentAlertLevel; }}

	public List<SiteTrait> traits { get { return m_traits; } }

	public List<AssetSlot> assets {get{ return m_assets; }}

	public List<Player.ActorSlot> agents {get{ return m_agents; }}

	public int regionID {get{ return m_regionID; }set{ m_regionID = value; }}

	public int alertLevelChange {get{ return m_alertLevelChange; } set{m_alertLevelChange = value;}}

	public Dictionary<int, AffinitySlot> affinityList { get { return m_affinityList; }}

	public EffectPool effectPool {get{ return m_effectPool; }}
}
