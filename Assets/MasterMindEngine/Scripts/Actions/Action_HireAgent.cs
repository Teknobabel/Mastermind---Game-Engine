using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_HireAgent : Action {

	public int m_playerNumber = -1;
	public int m_henchmenID = -1;

	public override void ExecuteAction ()
	{
		if (GameEngine.instance.game.playerList.ContainsKey (m_playerNumber)) {

			Player player = GameEngine.instance.game.playerList [m_playerNumber];

			// find henchmen in hiring pool

			foreach (Player.ActorSlot aSlot in player.hiringPool.m_hireSlots) {

				if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty && aSlot.m_actor.id == m_henchmenID) {

					// remove from hiring pool

					Actor thisHenchmen = aSlot.m_actor;

					aSlot.RemoveHenchmen ();

					// add to henchmen pool

					foreach (Player.ActorSlot newHenchmenSlot in player.henchmenPool.m_henchmenSlots) {

						if (newHenchmenSlot.m_state == Player.ActorSlot.ActorSlotState.Empty) {

							newHenchmenSlot.SetHenchmen (thisHenchmen);

							// notify UI for updating

							GameController.instance.Notify (player, GameEvent.Player_HiringPoolChanged);
							GameController.instance.Notify (player, GameEvent.Player_HenchmenPoolChanged);

							break;
						}
					}

					break;
				}
			}

		} else {
			Debug.Log ("Player ID not found");
		}
	}
}
