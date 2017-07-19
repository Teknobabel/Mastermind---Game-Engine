using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurnPhase_AgentPhase : TurnPhase {

//	public override IEnumerator StartTurnPhase ()
//	{
//		Debug.Log ("Starting Agent Phase");
//		yield return null;
//	}

	public override void DoTurnPhase ()
	{
		Debug.Log ("Starting Agent Phase");

		// do AI for agents not on a mission

		// resolve missions

		List<MissionPlan> completedMissions = new List<MissionPlan> ();

		foreach (KeyValuePair<int, Player> pair in GameEngine.instance.game.playerList) {

			List<MissionPlan> missions = new List<MissionPlan> ();

			foreach (MissionPlan mp in pair.Value.currentMissions) {

				missions.Add (mp);
			}

			while (missions.Count > 0) {

				MissionPlan thisMP = missions [0];
				missions.RemoveAt (0);

				Action_EvaluateMission evalMission = new Action_EvaluateMission ();
				evalMission.m_missionPlan = thisMP;
				evalMission.m_playerID = pair.Value.id;

				GameController.instance.ProcessAction (evalMission);

				if (thisMP.m_state == MissionPlan.State.Complete) {

					completedMissions.Add (thisMP);
				}
			}

			foreach (MissionPlan mp in completedMissions) {

				pair.Value.RemoveMission (mp);
			}
		}

		// spawn new agents if needed

//		GameEngine.instance.ProgressTurn ();
		Debug.Log ("Ending Agent Phase");
	}

//	public override IEnumerator EndTurnPhase ()
//	{
//		Debug.Log ("Ending Agent Phase");
//		yield return null;
//	}
}
