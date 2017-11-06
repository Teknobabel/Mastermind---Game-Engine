using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_CantPayHenchmen : Action {

	public int m_playerID = -1;
	public Actor m_henchmen = null;

	public override void ExecuteAction ()
	{
		Player player = GameEngine.instance.game.playerList [m_playerID];

		// add notification that henchmen didn't get paid

		string title = "Henchmen Not Payed";
		string message = "You could not afford to pay " + m_henchmen.m_actorName + " this Turn.";

		player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Contacts, false, -1);
		m_henchmen.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Contacts, false, -1);

		// lower affinity for player

		m_henchmen.UpdateAffinity (player.id, -10, (IAffinity)player);

		int playerAffinity = m_henchmen.GetAffinityScore (player.id, (IAffinity)player);

		// if affinity is critical, chance to go rogue

		if (playerAffinity <= -40) {

			float rogueChance = 0.2f;

			if (playerAffinity <= -60) {

				rogueChance += 0.2f;
			}

			if (Random.Range(0.0f, 1.0f) <= rogueChance)
			{
				// henchmen leaves player organization

				Action_FireAgent fireAgent = new Action_FireAgent ();
				fireAgent.m_playerNumber = 0;
				fireAgent.m_henchmenID = m_henchmen.id;
				GameController.instance.ProcessAction (fireAgent);

				// chance to place intel in world

				float intelChance = 0.2f;

				if (playerAffinity <= -50) {

					intelChance += 0.2f;
				}

				if (Random.Range (0.0f, 1.0f) <= intelChance) {

					Action_PlaceIntel placeIntel = new Action_PlaceIntel ();
					placeIntel.m_playerID = 0;
					GameController.instance.ProcessAction (placeIntel);

				}

				// henchmen added to agents

			}

		} else if (playerAffinity <= -20)
		{
			// else if affinity is low, chance to leave

			float leaveChance = 0.2f;

			if (playerAffinity <= -30) {

				leaveChance += 0.2f;
			}

			if (Random.Range (0.0f, 1.0f) <= leaveChance) {

				Action_FireAgent fireAgent = new Action_FireAgent ();
				fireAgent.m_playerNumber = 0;
				fireAgent.m_henchmenID = m_henchmen.id;
				GameController.instance.ProcessAction (fireAgent);
			}
		}

		// if affinity is low, chance to gain a Hostile trait

		float hostileChance = 0.2f;

		if (playerAffinity <= -30) {

			hostileChance += 0.2f;
		}

		if (playerAffinity <= -40) {

			hostileChance += 0.2f;
		}

		if (Random.Range (0.0f, 1.0f) <= hostileChance) {

			bool alreadyHostile = false;

			foreach (Trait t in m_henchmen.traits) {

				if (t.m_name == "Hostile") {

					alreadyHostile = true;
					break;
				}
			}

			if (!alreadyHostile) {

				Trait_LowerMissionSuccess hostileTrait = new Trait_LowerMissionSuccess ();
				hostileTrait.m_name = "Hostile";
				hostileTrait.m_type = Trait.Type.Dynamic;
				hostileTrait.m_bonus = -10;

				Action_GainTrait newTrait = new Action_GainTrait ();
				newTrait.m_newTrait = hostileTrait;
				newTrait.m_actor = m_henchmen;
				newTrait.m_playerID = 0;
				GameController.instance.ProcessAction (newTrait);
			}
		}

	}
}
