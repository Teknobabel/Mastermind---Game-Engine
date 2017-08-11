using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Actor : ScriptableObject, IBaseObject {

	public string m_actorName = "Actor";

	public Trait[] m_startingTraits;

	public StatusTrait m_status;

	public int
	m_startingCost = 1,
	m_turnCost = 1,
	m_rank = 1;

	public Texture
	m_portrait_Compact,
	m_portrait_Large;

	private List<Trait> m_traits = new List<Trait>();
	private NotificationCenter m_notifications = new NotificationCenter();

	public void AddTrait (Trait newTrait)
	{
		m_traits.Add (newTrait);
	}

	public void Initialize ()
	{
		foreach (Trait t in m_startingTraits) {

			AddTrait (t);
		}

	}

	private int m_id = -1;

	public int id {get{ return m_id; } set{ m_id = value; }}
	public List<Trait> traits {get{ return m_traits; }}
	public NotificationCenter notifications {get{ return m_notifications;}}
}
