using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Actor : ScriptableObject, IBaseObject {

	public string m_actorName = "Actor";

	public Trait[] m_startingTraits;

	public int
	m_startingCost = 1,
	m_turnCost = 1;

	public Texture
	m_portrait_Compact,
	m_portrait_Large;

	private int m_id = -1;

	public int id {get{ return m_id; } set{ m_id = value; }}
}
