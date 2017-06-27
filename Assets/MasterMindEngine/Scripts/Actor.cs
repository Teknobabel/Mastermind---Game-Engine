using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Actor : ScriptableObject {

	public string m_actorName = "Actor";

	public Trait[] m_startingTraits;

	public int
	m_startingCost = 1,
	m_turnCost = 1;

	public Texture
	m_portrait_Compact,
	m_portrait_Large;
}
