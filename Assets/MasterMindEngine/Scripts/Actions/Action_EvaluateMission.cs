using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_EvaluateMission : Action {

	public MissionPlan m_missionPlan;

	public int m_playerID = -1;

	public override void ExecuteAction ()
	{

		m_missionPlan.m_turnNumber++;

		Player player = GameEngine.instance.game.playerList [m_playerID];

		if (m_missionPlan.m_turnNumber >= m_missionPlan.m_currentMission.m_duration) {

			Debug.Log ("Mission Complete");

			// complete mission

			m_missionPlan.m_state = MissionPlan.State.Complete;

			// determine success or failure

			float successChance = (float)m_missionPlan.m_successChance;

			if (Random.Range (1.0f, 100.0f) <= successChance) {

				// mission successful

				m_missionPlan.m_result = MissionPlan.Result.Success;

			} else {

				// mission failure

				m_missionPlan.m_result = MissionPlan.Result.Fail;
			}

			if (GameEngine.instance.m_forceMissionSuccess) {
				m_missionPlan.m_result = MissionPlan.Result.Success;
			}

			m_missionPlan.m_currentMission.CompleteMission (m_missionPlan);

			// add to completed mission list

			MissionSummary newMissionSummary = new MissionSummary ();
			newMissionSummary.m_missionID = m_missionPlan.m_missionID;
			newMissionSummary.m_mission = m_missionPlan.m_currentMission;
			newMissionSummary.m_missionResult = m_missionPlan.m_result;

			foreach (Player.ActorSlot aSlot in m_missionPlan.m_actorSlots) {

				if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {

					newMissionSummary.m_participatingActors.Add (aSlot.m_actor);
				}
			}

			player.missionsCompletedThisTurn.Add (newMissionSummary);

			// update infamy and site alert level

			int alertGain = 0;
			int infamyGain = 0;

			switch (m_missionPlan.m_currentMission.m_infamy) {

			case Mission.InfamyLevel.Low:

				if (m_missionPlan.m_result == MissionPlan.Result.Fail) {
					alertGain += 1;
				} else {
					infamyGain += 1;
				}

				break;

			case Mission.InfamyLevel.Medium:

				alertGain += 1;
				infamyGain += 1;

				if (m_missionPlan.m_result == MissionPlan.Result.Fail) {
					alertGain += 2;
				} else {
					infamyGain += 2;
				}

				break;
			case Mission.InfamyLevel.High:

				alertGain += 2;
				infamyGain += 2;

				if (m_missionPlan.m_result == MissionPlan.Result.Fail) {
					alertGain += 3;
				} else {
					infamyGain += 3;
				}

				break;
			}

			if (alertGain > 0 && m_missionPlan.m_missionSite != null) {

				Action_ChangeAlertLevel alertLevel = new Action_ChangeAlertLevel ();
				alertLevel.m_playerID = 0;
				alertLevel.m_siteID = m_missionPlan.m_missionSite.id;
				alertLevel.m_amount = alertGain;
				alertLevel.m_missionID = m_missionPlan.m_missionID;
				GameController.instance.ProcessAction (alertLevel);
			}

			if (infamyGain > 0) {

				Action_GainInfamy gainInfamy = new Action_GainInfamy ();
				gainInfamy.m_playerID = 0;
				gainInfamy.m_amount = infamyGain;
				gainInfamy.m_missionID = m_missionPlan.m_missionID;
				GameController.instance.ProcessAction (gainInfamy);
			}

			// consume any required assets if mission success, or free them up if mission fail

			while (m_missionPlan.m_linkedPlayerAssets.Count > 0) {

				Site.AssetSlot aSlot = m_missionPlan.m_linkedPlayerAssets [0];
				m_missionPlan.m_linkedPlayerAssets.RemoveAt (0);

				if (m_missionPlan.m_result == MissionPlan.Result.Success) {

					Action_RemoveAsset removeAsset = new Action_RemoveAsset ();
					removeAsset.m_playerID = 0;
					removeAsset.m_assetSlot = aSlot;
					removeAsset.m_missionID = m_missionPlan.m_missionID;
					GameController.instance.ProcessAction (removeAsset);

				} else {

					aSlot.m_state = Site.AssetSlot.State.Revealed;
				}
			}

			// check for learning new traits

			m_missionPlan.m_currentMission.CheckForLearningTraits (m_missionPlan);

			// free up participating henchmen

			foreach (Player.ActorSlot aSlot in m_missionPlan.m_actorSlots) {
				
				aSlot.m_state = Player.ActorSlot.ActorSlotState.Active;
			}

			// free up floor slot in lair

			if (m_missionPlan.m_floorSlot != null) {

				if (!m_missionPlan.m_doRepeat) {
					m_missionPlan.m_currentMission = null;
				}

				m_missionPlan.m_result = MissionPlan.Result.None;
				m_missionPlan.m_floorSlot.m_state = Lair.FloorSlot.FloorState.Idle;
			}

			// reset plan to default values

			if (!m_missionPlan.m_doRepeat) {
				
				m_missionPlan.m_actorSlots.Clear ();
				m_missionPlan.m_missionSite = null;
				m_missionPlan.m_currentAsset = null;
			}
			m_missionPlan.m_new = false;
			m_missionPlan.m_successChance = 0;
			m_missionPlan.m_turnNumber = 0;
			m_missionPlan.m_requiredTraits.Clear ();
			m_missionPlan.m_matchingTraits.Clear ();
			m_missionPlan.m_matchingFloors.Clear ();
			m_missionPlan.m_requiredFloors.Clear ();
			m_missionPlan.m_effects.Clear ();

			GameController.instance.Notify (player, GameEvent.Player_MissionCompleted);

		} else {

			string title = "Mission Report";
			string message = "Mission: " + m_missionPlan.m_currentMission.m_name + " is in progress.";

//			if (m_missionPlan.m_turnNumber == 1) {
//
//				title = "New Mission Begins";
//				message = "Mission: " + m_missionPlan.m_currentMission.m_name + " is underway.";
//			}

			message += "(" + m_missionPlan.m_turnNumber.ToString () + "/" + m_missionPlan.m_currentMission.m_duration.ToString () + ")";

			player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Missions, false, m_missionID);

			foreach (Player.ActorSlot aSlot in m_missionPlan.m_actorSlots) {

				if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {

					aSlot.m_actor.notifications.AddNotification(GameController.instance.GetTurnNumber(), title, message, EventLocation.Missions, false, m_missionID);
				}
			}
		}
	}
}
