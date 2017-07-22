using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Region : ScriptableObject, IBaseObject {

	private int m_id = -1;

	public string m_regionName = "Region";

	public List<Site> m_startingSites = new List<Site>();

	public int id {get{ return m_id; } set{ m_id = value; }}

	private List<Site> m_sites = new List<Site>();

	public void AddSite (Site s)
	{
		m_sites.Add (s);
	}

	public List<Site> sites {get{ return m_sites;}}
}
