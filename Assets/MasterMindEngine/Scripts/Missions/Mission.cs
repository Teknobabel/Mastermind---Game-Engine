using System.Collections;
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

	public enum InfamyLevel
	{
		Low,
		Medium,
		High,
	}

	public string 
	m_name = "Null",
	m_description = "Null";

	public TargetType m_targetType = TargetType.Site;

	public Trait[] m_requiredTraits;
	public Asset[] m_requiredAssets;
	public Floor[] m_requiredFloors;

	public InfamyLevel m_infamy = InfamyLevel.Low;

	public int 
	m_cost = 1,
	m_duration = 1,
	m_minFloorLevel = 1;

	public virtual bool IsValid (MissionPlan plan)
	{
		if (plan.m_floorSlot != null && plan.m_floorSlot.m_floor.level >= m_minFloorLevel) {
			return true;
		} else if (plan.m_goal != null) {
			return true;
		} else if (plan.m_missionOptions.Count > 0) {

			foreach (Lair.FloorSlot fslot in plan.m_missionOptions) {

				foreach (Mission m in fslot.m_floor.m_missions) {

					if (m.m_name == m_name) {

						if (fslot.m_floor.level >= m_minFloorLevel) {

							return true;

						} else {

							return false;
						}

					}
				}
			}

		}
		return false;
	}

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

			if (plan.m_goal != null) {

				plan.m_goal.m_state = OmegaPlan.OPGoal.State.Incomplete;
			}
		}

		player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Missions, false, plan.m_missionID);

		foreach (Player.ActorSlot aSlot in plan.m_actorSlots) {

			if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {

				aSlot.m_actor.notifications.AddNotification(GameController.instance.GetTurnNumber(), title, message, EventLocation.Missions, false, plan.m_missionID);
			}
		}
	}

	public void CheckForLearningTraits (MissionPlan plan)
	{
		float baseLearnChance = 0.1f;
		float rank1TraitBonus = 0.1f;
		float rank2TraitBonus = -0.05f;
		float rank3TraitBonus = -0.1f;
		float rank4TraitBonus = -0.2f;
		float linkedSkillBonus = 0.1f; // if the henchmen has the skill linked to the trait
		float assistanceBonus = 0.1f; // granted if another participating henchmen has the trait
		float missionFailureBonus = 0.05f;

		foreach (Player.ActorSlot aSlot in plan.m_actorSlots) {

			foreach (Trait t in plan.m_requiredTraits) {

				if (!aSlot.m_actor.traits.Contains (t)) {

					float learnChance = baseLearnChance;

					// check rank of trait

					switch (t.m_rank) {

					case 1:
						learnChance += rank1TraitBonus;
						break;
					case 2:
						learnChance += rank2TraitBonus;
						break;
					case 3:
						learnChance += rank3TraitBonus;
						break;
					case 4:
						learnChance += rank4TraitBonus;
						break;
					}

					// see if henchmen has the linked skill

					if (t.m_linkedSkill != null && aSlot.m_actor.traits.Contains (t.m_linkedSkill)) {

						learnChance += linkedSkillBonus;
					}

					// see if another participating henchmen has the skill

					foreach (Player.ActorSlot aSlot2 in plan.m_actorSlots) {

						if (aSlot2.m_actor.traits.Contains (t)) {

							learnChance += assistanceBonus;
						}
					}

					// see if mission was a failure

					if (plan.m_result == MissionPlan.Result.Fail) {

						learnChance += missionFailureBonus;
					}

					if (learnChance > 0.0f) {
						
						Debug.Log ("Learn Trait Chance: " + learnChance);

						if (Random.Range (0.0f, 1.0f) <= learnChance) {

							Action_GainTrait newTrait = new Action_GainTrait ();
							newTrait.m_playerID = 0;
							newTrait.m_newTrait = t;
							newTrait.m_actor = aSlot.m_actor;
							newTrait.m_missionID = plan.m_missionID;
							GameController.instance.ProcessAction (newTrait);
						}
					}
				}

			}
		}

	}

}
