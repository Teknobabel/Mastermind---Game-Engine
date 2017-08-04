using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_GainTrait : Mission {

	public Trait m_trait;

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		if (plan.m_result == MissionPlan.Result.Success && plan.m_targetActor.m_state != Player.ActorSlot.ActorSlotState.Empty) {

			Action_GainTrait gainTrait = new Action_GainTrait ();
			gainTrait.m_playerID = 0;
			gainTrait.m_actor = plan.m_targetActor.m_actor;
			gainTrait.m_newTrait = m_trait;
			GameController.instance.ProcessAction (gainTrait);
		}
	}
}
