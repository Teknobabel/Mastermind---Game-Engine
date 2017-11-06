using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicTrait_LocationLink : Trait {

	public enum LinkType
	{
		None,
		Citizen,
		Wanted,
	}

	public LinkType m_linkType = LinkType.None;

	public Site m_linkedSite = null;

	public override string GetName ()
	{
		string s = "";

		if (m_linkType == LinkType.Citizen) {
			
			s += "Citizen of " + m_linkedSite.m_siteName;

		} else if (m_linkType == LinkType.Wanted) {
			
			s += "Wanted in " + m_linkedSite.m_siteName;
		}

		return s;
	}

	public override int GetBonus (MissionPlan plan)
	{
		int bonus = 0;

		if (plan.m_missionSite != null && plan.m_missionSite.m_siteName == m_linkedSite.m_siteName) {

			if (m_linkType == LinkType.Citizen) {

				bonus += (5 * m_rank);

			} else if (m_linkType == LinkType.Wanted) {

				bonus -= (5 * m_rank);
			}

		}
			
		return bonus;
	}
}
