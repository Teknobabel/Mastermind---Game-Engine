﻿using System.Collections;
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
		Actor,
		Region,
		SiteTrait,
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
		Player player = GameEngine.instance.game.playerList [0];

		string title = "Mission Completed";
		string message = "Mission: " + plan.m_currentMission.m_name;

		if (plan.m_result == MissionPlan.Result.Success) {

			message += " is a success!";

			if (plan.m_goal != null) {

				plan.m_goal.m_state = OmegaPlan.OPGoal.State.Complete;
				GameController.instance.Notify (player, GameEvent.Player_OmegaPlanGoalCompleted);
			}

		} else if (plan.m_result == MissionPlan.Result.Fail) {

			message += " is a failure.";

		}

		player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message);

		foreach (Player.ActorSlot aSlot in plan.m_actorSlots) {

			if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {

				aSlot.m_actor.notifications.AddNotification(GameController.instance.GetTurnNumber(), title, message);
			}
		}
	}
}
