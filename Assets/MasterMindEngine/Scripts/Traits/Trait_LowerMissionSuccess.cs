using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trait_LowerMissionSuccess : Trait {

	public int m_bonus = -10;

	public override string GetDescription ()
	{
		return "Mission success chance is lowered by " + m_bonus.ToString() + "%.";
	}

	public override int GetBonus (MissionPlan plan)
	{
		return m_bonus;
	}
}
