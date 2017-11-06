using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Trait : ScriptableObject {

	public enum Type {
		None,
		Skill,
		Gift,
		Flaw,
		Dynamic,
		Status,
		Resource,
		Site,
	}

	public string m_name;

	public Type m_type = Type.None;

	public int m_rank = 1;

	public Trait m_linkedSkill;

	public int m_combatValue = 0;

	public virtual string GetName()
	{
		return m_name;
	}

	public virtual string GetDescription ()
	{
		return "Null";
	}

	public virtual int GetBonus (MissionPlan plan)
	{
		return 0;
	}
}
