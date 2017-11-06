using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_Agent_Hide : Mission {

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		if (plan.m_targetActor != null) {

			plan.m_targetActor.m_visibilityState = Player.ActorSlot.VisibilityState.Hidden;
		}
	}
}
