using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Site : ScriptableObject, IBaseObject {

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
		}

		public Asset m_asset;
		public State m_state;
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

	public Asset[] m_startingAssets;

	public List<RandomAssetList> m_randomStartingAssets = new List<RandomAssetList>();

	public SiteTrait[] m_startingTraits;

	public List<RandomTraitList> m_randomStartingTraits = new List<RandomTraitList>();

	private List<AssetSlot> m_assets = new List<AssetSlot>();

	private List<SiteTrait> m_traits = new List<SiteTrait>();

	private int m_id = -1;

	public void Initialize ()
	{
		foreach (Asset a in m_startingAssets) {

			AddAsset (a);
		}

		foreach (RandomAssetList ral in m_randomStartingAssets) {

			if (ral.m_assets.Count > 0) {

				int rand = Random.Range (0, ral.m_assets.Count);
				Asset a = ral.m_assets [rand];
				AddAsset (a);
			}
		}

		foreach (SiteTrait t in m_startingTraits) {

			AddTrait (t);
		}

		foreach (RandomTraitList rtl in m_randomStartingTraits) {

			if (rtl.m_traits.Count > 0) {

				int rand = Random.Range (0, rtl.m_traits.Count);
				SiteTrait t = rtl.m_traits [rand];
				AddTrait (t);
			}
		}
	}

	public void AddTrait (SiteTrait newTrait)
	{
		m_traits.Add (newTrait);
	}

	public void AddAsset (Asset newAsset)
	{
		AssetSlot aSlot = new AssetSlot ();
		aSlot.m_asset = newAsset;
		aSlot.m_state = AssetSlot.State.Hidden;

		m_assets.Add (aSlot);
	}

	public int id {get{ return m_id; } set{ m_id = value; }}
	public List<SiteTrait> traits { get { return m_traits; } }
	public List<AssetSlot> assets {get{ return m_assets; }}
}
