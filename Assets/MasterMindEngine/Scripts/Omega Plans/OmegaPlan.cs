﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class OmegaPlan : ScriptableObject, IObserver {

	[System.Serializable]
	public class OPGoal
	{
		public enum State
		{
			Incomplete,
			Complete,
			Locked,
			InProgress,
		}

		public Mission m_mission;

		public int m_numActorSlots = 3;

		private MissionPlan m_plan;

		public Asset[] m_mandatoryAssets;

		public bool m_new = true;

		public State m_state = State.Incomplete;

		public MissionPlan plan {get{ return m_plan; } set{ m_plan = value;}}
	}

	[System.Serializable]
	public class Phase {

		public List<OPGoal> m_goals = new List<OPGoal>();
		//		public List<Mission> m_goals;

		//		public List<Asset> m_mandatoryAssets;
	}

	public string m_name;

	public Phase[] m_startingPhases;

	private List<Phase> m_phases = new List<Phase>();

	private int m_currentPhase = 0;

	public void Initialize ()
	{
		for (int i = 0; i < m_startingPhases.Length; i++) {

			Phase p = m_startingPhases [i];
			Phase newPhase = new Phase ();

			foreach (OPGoal goal in p.m_goals) {

				OPGoal newGoal = new OPGoal ();
				newGoal.m_mission = goal.m_mission;
				newGoal.m_mandatoryAssets = goal.m_mandatoryAssets;
				newGoal.plan = new MissionPlan ();
				newGoal.plan.m_goal = newGoal;
				newGoal.plan.m_allowRepeat = false;

				//				for (int j = 0; j < 3; j++) {
				//
				//					Player.ActorSlot aSlot = new Player.ActorSlot ();
				//					aSlot.m_state = Player.ActorSlot.ActorSlotState.Empty;
				//					newGoal.plan.m_actorSlots.Add (aSlot);
				//				}

				newGoal.plan.m_currentMission = goal.m_mission;

				if (i != m_currentPhase) {

					newGoal.m_state = OPGoal.State.Locked;
					newGoal.m_new = false;
				}

				newPhase.m_goals.Add (newGoal);
				GameController.instance.CompileMission (newGoal.plan);
			}

			m_phases.Add (newPhase);
		}

		GameController.instance.AddObserver (this);
	}

	public List<Asset> GetNeededAssets ()
	{
		List<Asset> neededAssets = new List<Asset> ();

		foreach (Phase phase in m_phases) {

			foreach (OPGoal goal in phase.m_goals) {

				if (goal.m_state != OPGoal.State.Complete) {

					foreach (Asset a in goal.m_mission.m_requiredAssets) {

						if (!neededAssets.Contains (a)) {

							neededAssets.Add (a);
						}
					}
				}
			}
		}

		return neededAssets;
	}

	public void OnNotify (ISubject subject, GameEvent thisGameEvent)
	{
		switch (thisGameEvent) {
		case GameEvent.Player_OmegaPlanGoalCompleted:

			Player player = (Player)subject;

			// check if all goals are completed

			bool allGoalsComplete = true;

			Phase thisPhase = m_phases [m_currentPhase];

			foreach (OPGoal g in thisPhase.m_goals) {

				if (g.m_state != OPGoal.State.Complete) {

					allGoalsComplete = false;
					break;
				}
			}

			// if all goals completed, move to next phase or win game

			if (allGoalsComplete) {

				string title = "Phase Completed";
				string message = "Omega Plan Phase " + (m_currentPhase+1).ToString() + " Completed!";
				player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.OmegaPlan, false, -1);

				if (m_currentPhase + 1 < m_phases.Count) {

					m_currentPhase++;

					Phase newPhase = m_phases [m_currentPhase];

					foreach (OPGoal g in newPhase.m_goals) {
						
						g.m_new = true;
						g.m_state = OPGoal.State.Incomplete;
					}

					title = "Phase Unlocked";
					message = "Omega Plan Phase " + (m_currentPhase+1).ToString() + " has been unlocked.";
					player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.OmegaPlan, false, -1);

					GameController.instance.Notify (player, GameEvent.Player_OmegaPlanChanged);

					Player.EventSummaryAlert eventSummary = new Player.EventSummaryAlert ();
					eventSummary.m_eventType = Player.EventSummaryAlert.EventType.OPPhaseComplete;
					eventSummary.m_title = title;
					eventSummary.m_message = message;
					player.thisTurnsAlerts.Add (eventSummary);

				} else {

				}

			}

			break;
		}
	}

	public List<Phase> phases {get{return m_phases;}}
	public int currentPhase {get{ return m_currentPhase; } set{ m_currentPhase = value; }}
}
