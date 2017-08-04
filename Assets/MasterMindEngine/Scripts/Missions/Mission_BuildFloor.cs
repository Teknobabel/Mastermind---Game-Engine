using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_BuildFloor : Mission {

	public Floor m_floor;

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		if (plan.m_result == MissionPlan.Result.Success) {

			Action_BuildNewFloor buildFloor = new Action_BuildNewFloor ();
			buildFloor.m_floor = m_floor;
			buildFloor.m_player = GameEngine.instance.game.playerList [0];
			GameController.instance.ProcessAction (buildFloor);

		}
	}
}