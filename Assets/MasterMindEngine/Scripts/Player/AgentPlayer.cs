using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPlayer : Player {

	private int m_lvl1Infamy = 10;
	private int m_lvl2Infamy = 20;
	private int m_lvl3Infamy = 40;
	private int m_lvl4Infamy = 60;
	private int m_lvl5Infamy = 80;
	private int m_lvl1AgentCount = 1;
	private int m_lvl2AgentCount = 3;
	private int m_lvl3AgentCount = 5;
	private int m_lvl4AgentCount = 7;
	private int m_lvl5AgentCount = 9;

	private List<Mission> m_missionBank = new List<Mission> ();

	private IAgentState m_currentAgentState = new AgentState_Idle ();

	public override void Initialize ()
	{
		base.Initialize ();

		m_missionBank.Add (ScriptableObject.CreateInstance<Mission_Agent_MoveToSite> ());
		m_missionBank.Add (ScriptableObject.CreateInstance<Mission_Agent_CollectIntel> ());
		m_missionBank.Add (ScriptableObject.CreateInstance<Mission_Agent_DisruptMission> ());
		m_missionBank.Add (ScriptableObject.CreateInstance<Mission_Agent_Hide> ());
	}

	public void DoAgentsTurn ()
	{
		Debug.Log ("Executing Agents Turn");

		foreach (Player.ActorSlot aSlot in m_henchmenPool.m_henchmenSlots) {

			if (aSlot.m_state != ActorSlot.ActorSlotState.Empty) {

				m_currentAgentState.DoTurn (aSlot, this);
			}
		}


		// spawn new agents if needed

		int infamy = GameController.instance.game.playerList [0].infamy;
		int numAgents = 0;

		if (infamy >= m_lvl5Infamy) {

			numAgents = m_lvl5AgentCount;
		} else if (infamy >= m_lvl4Infamy) {

			numAgents = m_lvl4AgentCount;
		} else if (infamy >= m_lvl3Infamy) {

			numAgents = m_lvl3AgentCount;
		} else if (infamy >= m_lvl2Infamy) {

			numAgents = m_lvl2AgentCount;
		} else if (infamy >= m_lvl1Infamy) {

			numAgents = m_lvl1AgentCount;
		}

		int currentAgentCount = 0;

		foreach (Player.ActorSlot aSlot in m_henchmenPool.m_henchmenSlots) {

			if (aSlot.m_state != ActorSlot.ActorSlotState.Empty) {
				currentAgentCount++;
			}
		}

		if (currentAgentCount < numAgents) {

			int agentsToHire = numAgents - currentAgentCount;

			List<ActorSlot> validAgents = new List<ActorSlot> ();

			foreach (Player.ActorSlot aSlot in m_hiringPool.m_hireSlots) {

				if (aSlot.m_state != ActorSlot.ActorSlotState.Empty) {

					validAgents.Add (aSlot);
				}
			}

			for (int i = 0; i < agentsToHire; i++) {

				if (validAgents.Count > 0) {

					int r = Random.Range (0, validAgents.Count);

					Actor agent = validAgents [r].m_actor;
					validAgents.RemoveAt(r);

					Action_HireAgent hireAgent = new Action_HireAgent ();
					hireAgent.m_henchmenID = agent.id;
					hireAgent.m_playerNumber = id;
					GameController.instance.ProcessAction (hireAgent);
				}
			}
		}
	}

	public List<Mission> missionBank {get{return m_missionBank;}}
}
