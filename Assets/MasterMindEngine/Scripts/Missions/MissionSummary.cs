using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSummary {

	public Mission m_mission;

	public int m_missionID = -1;

	public MissionPlan.Result m_missionResult;

	public List<Actor> m_participatingActors = new List<Actor>();
}
