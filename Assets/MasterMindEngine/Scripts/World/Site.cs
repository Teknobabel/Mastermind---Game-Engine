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

	[System.Serializable]
	public struct RandomAssetList
	{
		public List<Asset> m_assets;
	}

	[System.Serializable]
	public struct RandomTraitList
	{
		public List<Trait> m_traits;
	}

	public string m_siteName = "Site";

	public Type m_type = Type.None;

	public int m_maxAlertLevel = 5;

	public Asset[] m_startingAssets;

	public List<RandomAssetList> m_randomStartingAssets = new List<RandomAssetList>();

	public Trait[] m_startingTraits;

	public List<RandomTraitList> m_randomStartingTraits = new List<RandomTraitList>();

	private List<Asset> m_assets = new List<Asset>();

	private List<Trait> m_traits = new List<Trait>();

	private int m_id = -1;

	public void AddTrait (Trait newTrait)
	{

	}

	public void AddAsset (Asset newAsset)
	{

	}

	public int id {get{ return m_id; } set{ m_id = value; }}
}
