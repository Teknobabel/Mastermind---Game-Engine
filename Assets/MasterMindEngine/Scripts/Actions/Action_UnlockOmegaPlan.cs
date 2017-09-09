using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_UnlockOmegaPlan : Action {

	public Player m_player = null;

	public List<OmegaPlan> m_omegaPlans = new List<OmegaPlan> ();

	public override void ExecuteAction ()
	{
		OmegaPlan plan = null;

		if (m_omegaPlans.Count > 1) {

			plan = (OmegaPlan)m_omegaPlans[Random.Range(0, m_omegaPlans.Count)];
		} else if (m_omegaPlans.Count == 1) {

			plan = m_omegaPlans [0];
		}

		if (plan != null && m_player != null) {

			ScriptableObject op = plan;
			OmegaPlan newOmegaPlan = (OmegaPlan)Object.Instantiate (op);
			newOmegaPlan.Initialize ();
			m_player.AddOmegaPlan (newOmegaPlan);

			Debug.Log ("Unlocking Omega Plan: " + newOmegaPlan.m_name);

			string title = "New Omega Plan Unlocked";
			string message = "Omega Plan: " + newOmegaPlan.m_name + " has been unlocked";

			m_player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.OmegaPlan, false, m_missionID);
		}
	}
}
