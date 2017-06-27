using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Region : ScriptableObject {

	public string m_regionName = "Region";

	public List<Site> m_sites = new List<Site>();

}
