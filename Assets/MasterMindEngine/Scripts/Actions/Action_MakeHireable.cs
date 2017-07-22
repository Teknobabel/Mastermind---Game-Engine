using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_MakeHireable : Action {

	public Player m_player = null;

	public Actor m_henchmen = null;

	public override void ExecuteAction ()
	{
		Debug.Log ("Henchmen: " + m_henchmen.m_actorName + " now available for hire");

		if (m_player.hiringPool.m_availableHenchmen.Contains(m_henchmen))
		{
			m_player.hiringPool.m_availableHenchmen.Remove (m_henchmen);
		}

		foreach (Player.ActorSlot aSlot in m_player.hiringPool.m_hireSlots) {

			if (aSlot.m_state == Player.ActorSlot.ActorSlotState.Empty) {

				aSlot.SetHenchmen (m_henchmen);

				string title = "New Henchmen for Hire";
				string message = m_henchmen.m_actorName + " is now available to Hire.";

				m_player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message);

				GameController.instance.Notify (m_player, GameEvent.Player_HiringPoolChanged);

				break;
			}
		}
	}
}
