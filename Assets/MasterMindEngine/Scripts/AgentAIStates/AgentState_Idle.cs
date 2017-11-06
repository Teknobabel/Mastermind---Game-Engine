using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentState_Idle : IAgentState {

	public override void DoTurn (Player.ActorSlot aSlot, Player agentPlayer)
	{
		Debug.Log ("Executing turn for: " + aSlot.m_actor.m_actorName);

		// if not at a site, add to a random site

		if (aSlot.m_currentSite == null) {

			GoToRandomSite (aSlot, agentPlayer);

			return;
		}

		// if there is intel in this region, chance to take it

		Site.AssetSlot intel = null;

		foreach (Site.AssetSlot assetSlot in aSlot.m_currentSite.assets) {

			if (assetSlot.m_asset != null && assetSlot.m_asset.m_name == "Intel") {

				intel = assetSlot;
				break;
			}
		}
			
		if (intel != null) {

			Mission collectIntel = ((AgentPlayer)agentPlayer).missionBank [1];
			collectIntel.m_duration = 1;
			MissionPlan plan = new MissionPlan ();
			plan.m_actorSlots.Add (aSlot);
			plan.m_currentMission = collectIntel;
			plan.m_missionSite = aSlot.m_currentSite;
			plan.m_currentAsset = intel;
			plan.m_successChance = 100;
			agentPlayer.AddMission (plan);

			return;
		}

		// if visible, go into hiding

		if (aSlot.m_visibilityState == Player.ActorSlot.VisibilityState.Visible) {

			Mission hideAgent = ((AgentPlayer)agentPlayer).missionBank [3];
			hideAgent.m_duration = 1;
			MissionPlan plan = new MissionPlan ();
			plan.m_actorSlots.Add (aSlot);
			plan.m_targetActor = aSlot;
			plan.m_currentMission = hideAgent;
			plan.m_successChance = 100;
			agentPlayer.AddMission (plan);

			return;
		}

		// check if there are any missions taking place in this region

		List<MissionPlan> currentMissions = GameController.instance.GetMissions (0);
		MissionPlan missionToDisrupt = null;

		foreach (MissionPlan plan in currentMissions) {

			if (plan.m_missionSite != null && plan.m_missionSite.id == aSlot.m_currentSite.id) {

				missionToDisrupt = plan;
				break;
			}
		}

		// if so, attempt to disrupt

		if (missionToDisrupt != null) {

			Mission disruptMission = ((AgentPlayer)agentPlayer).missionBank [2];
			disruptMission.m_duration = 1;
			MissionPlan plan = new MissionPlan ();
			plan.m_actorSlots.Add (aSlot);
			plan.m_currentMission = disruptMission;
			plan.m_missionSite = missionToDisrupt.m_missionSite;
			plan.m_successChance = 100;
			agentPlayer.AddMission (plan);

			return;
		}

		// if not, chance to move to a new region

		float moveChance = 0.45f;

		if (Random.Range (0.0f, 1.0f) <= moveChance) {

			GoToRandomSite (aSlot, agentPlayer);
		}
	}

	private void GoToRandomSite (Player.ActorSlot aSlot, Player agentPlayer)
	{
		List<Site> validSites = new List<Site> ();

		foreach (KeyValuePair<int, Site> pair in GameController.instance.game.siteList) {

			if (aSlot.m_currentSite == null || (aSlot.m_currentSite != null && aSlot.m_currentSite.id != pair.Value.id)) {
				validSites.Add (pair.Value);
			}
		}

		if (validSites.Count > 0) {

			int r = Random.Range (0, validSites.Count);

			Site thisSite = validSites [r];

			Mission moveToSite = ((AgentPlayer)agentPlayer).missionBank [0];
			moveToSite.m_duration = 1;
			MissionPlan plan = new MissionPlan ();
			plan.m_currentMission = moveToSite;
			plan.m_targetActor = aSlot;
			plan.m_actorSlots.Add (aSlot);
			plan.m_missionSite = thisSite;
			plan.m_successChance = 100;
			agentPlayer.AddMission (plan);

			return;
		}
	}
}
