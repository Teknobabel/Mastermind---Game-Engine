using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_AddSlotToFLoor : Mission {

//	public int m_playerID = 0;

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		if (plan.m_result == MissionPlan.Result.Success) {
			
//			Player player = GameEngine.instance.game.playerList [m_playerID];

			Player.ActorSlot aSlot = new Player.ActorSlot ();
			aSlot.m_state = Player.ActorSlot.ActorSlotState.Empty;
			aSlot.m_new = true;
			plan.m_floorSlot.m_actorSlots.Add (aSlot);
		}
	}
}
