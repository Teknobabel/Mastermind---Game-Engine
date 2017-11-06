using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Actor : ScriptableObject, IBaseObject, IAffinity {

	public string m_actorName = "Actor";

	public Trait[] m_startingTraits;

	public StatusTrait m_status;

	private Dictionary<int, AffinitySlot> m_affinityList = new Dictionary<int, AffinitySlot>();

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

	private int m_id = -1;

	public int id {get{ return m_id; } set{ m_id = value; }}
	public List<Trait> traits {get{ return m_traits; }}
	public NotificationCenter notifications {get{ return m_notifications;}}
	public Dictionary<int, AffinitySlot> affinityList { get { return m_affinityList; }}
}
