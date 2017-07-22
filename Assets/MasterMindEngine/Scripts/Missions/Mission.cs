using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Mission : ScriptableObject {

	public enum TargetType
	{
		None,
		Site,
		Asset,
		Lair,
	}

	public string 
	m_name = "Null",
	m_description = "Null";

	public TargetType m_targetType = TargetType.Site;

	public Trait[] m_requiredTraits;

	public int 
	m_cost = 1,
	m_duration = 1;

	public virtual void CompleteMission (MissionPlan plan)
	{

	}
}
