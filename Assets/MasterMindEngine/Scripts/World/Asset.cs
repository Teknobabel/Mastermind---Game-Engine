using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Asset : ScriptableObject {

	public string m_name = "Null";

	public int m_rank = 0;

	public Site.Type m_preferedSiteType = Site.Type.None;

	public Trait[] m_requiredTraits;

}
