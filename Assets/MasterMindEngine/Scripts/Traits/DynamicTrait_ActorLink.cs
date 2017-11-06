using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicTrait_ActorLink : Trait {

	public enum LinkType
	{
		None,
		Ally,
		Rival
	}

	public LinkType m_linkType = LinkType.None;

	public Actor m_linkedActor = null;

	public override string GetName ()
	{
		string s = "";

		if (m_linkType == LinkType.Ally) {

			if (m_rank == 1) {
				
				s += "Ally of " + m_linkedActor.m_actorName;

			} else if (m_rank == 2) {

				s += "Partner of " + m_linkedActor.m_actorName;
			}

		} else if (m_linkType == LinkType.Rival) {

			if (m_rank == 1) {
				
				s += "Rival of " + m_linkedActor.m_actorName;

			} else if (m_rank == 2) {

				s += "Hatred of " + m_linkedActor.m_actorName;
			}
		}

		return s;
	}

	public override int GetBonus (MissionPlan plan)
	{
		int bonus = 0;

		foreach (Player.ActorSlot aSlot in plan.m_actorSlots) {

			if (aSlot.m_actor.m_actorName == m_linkedActor.m_actorName) {

				if (m_linkType == LinkType.Ally) {

					bonus += (5 * m_rank);

				} else if (m_linkType == LinkType.Rival) {

					bonus -= (5 * m_rank);

				}
			}
		}

		return bonus;
	}
}
