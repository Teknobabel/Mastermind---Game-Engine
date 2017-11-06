using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_Agent_DisruptMission : Mission {

	public override void CompleteMission (MissionPlan plan)
	{
		Debug.Log ("Moving Agent to new Site");

		base.CompleteMission (plan);

		// find mission at site

		List<MissionPlan> currentMissions = GameController.instance.GetMissions (0);
		MissionPlan missionToDisrupt = null;

		foreach (MissionPlan missionPlan in currentMissions) {

			if (missionPlan.m_missionSite != null && missionPlan.m_missionSite.id == plan.m_missionSite.id) {

				missionToDisrupt = missionPlan;
				break;
			}
		}

		// if mission is found, calculate combat values for each side

		if (missionToDisrupt != null) {

			string title = "Henchmen Attacked";
			string message = "Agents have attempted to disrupt your " + missionToDisrupt.m_currentMission.m_name + " Mission taking place in " + missionToDisrupt.m_missionSite.m_siteName + "!";

			// compare accumulated combat values, higher side wins

			int agentCombatPotential = 0;
			int henchmenCombatPotential = 0;

			foreach (Player.ActorSlot aSlot in missionToDisrupt.m_actorSlots) {

				foreach (Trait t in aSlot.m_actor.traits) {

					henchmenCombatPotential += t.m_combatValue;
				}

				henchmenCombatPotential += aSlot.m_actor.m_status.m_combatValue;
			}

			foreach (Player.ActorSlot aSlot in plan.m_actorSlots) {

				foreach (Trait t in aSlot.m_actor.traits) {

					agentCombatPotential += t.m_combatValue;
				}

				agentCombatPotential += aSlot.m_actor.m_status.m_combatValue;
			}

			agentCombatPotential += Random.Range (0, 6);
			henchmenCombatPotential += Random.Range (0, 6);

			// if henchmen lost, chance to disrupt mission
				
			float disruptionChance = 0.3f;

			if (agentCombatPotential >= 15) {
				disruptionChance += 0.25f;
			}
			if (agentCombatPotential >= 25) {
				disruptionChance += 0.25f;
			}

			if (agentCombatPotential > henchmenCombatPotential && Random.Range (0.0f, 1.0f) <= disruptionChance) {

				message += "\nThe Agents have succeeded!";
					
			} else {

				message += "\nYour Henchmen were able to hold them off.";
			}

			// losing side takes damage

			List<Player.ActorSlot> validTargets = new List<Player.ActorSlot> ();

			if (agentCombatPotential > henchmenCombatPotential) {

				foreach (Player.ActorSlot aSlot in missionToDisrupt.m_actorSlots) {

					validTargets.Add (aSlot);
				}
			} else {
				
				foreach (Player.ActorSlot aSlot in plan.m_actorSlots) {

					// if a bodyguard is present, they take the damage

					bool bodyguard = false;

					foreach (Trait t in aSlot.m_actor.traits) {

						if (t.m_name == "Bodyguard") {

							bodyguard = true;
							break;
						}
					}

					if (bodyguard) {
						validTargets.Clear ();
						validTargets.Add (aSlot);
						break;
					} else {
						validTargets.Add (aSlot);
					}
				}
			}

			if (validTargets.Count > 0) {

				Player.ActorSlot target = validTargets [Random.Range(0, validTargets.Count)];

				message += "\n" + target.m_actor.m_actorName + " is wounded!";

				StatusTrait newStatus = null;

				if (target.m_actor.m_status.m_name == "Normal") {

					newStatus = GameEngine.instance.m_statusTraits [1];
				} else if (target.m_actor.m_status.m_name == "Injured") {
					newStatus = GameEngine.instance.m_statusTraits [2];
				}

				if (newStatus != null) {

					Action_GainStatusTrait newStatusTrait = new Action_GainStatusTrait ();
					newStatusTrait.m_actor = target.m_actor;
					newStatusTrait.m_newStatus = newStatus;
//					newStatusTrait.m_playerID = 
					GameController.instance.ProcessAction(newStatusTrait);
				}
			}


			// notify player of attack
		}

	}
}
