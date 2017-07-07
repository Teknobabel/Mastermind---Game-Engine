using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_HireHenchmen : Action {

	public Player m_player = null;

	public Player.ActorSlot m_newSlot;

	public Actor m_henchmen = null;

	public int m_cost = 0;

	public override void ExecuteAction ()
	{
		Debug.Log ("Hiring henchmen: " + m_henchmen.m_actorName);

		foreach (Player.ActorSlot slot in m_player.henchmenPool.m_henchmenSlots) {

			if (slot.m_state == Player.ActorSlot.ActorSlotState.Empty) {

				m_player.SpendCommandPoints (m_cost);
				slot.SetHenchmen (m_henchmen);
				break;
			}
		}


	}
}
