using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_Agent_CollectIntel : Mission {

	public override void CompleteMission (MissionPlan plan)
	{
		Debug.Log ("Moving Agent to new Site");

		base.CompleteMission (plan);

		if (plan.m_targetActor.m_currentSite != null) {

			// make sure intel is still at site

			if (plan.m_missionSite.assets.Contains(plan.m_currentAsset))
			{

				// remove intel from site

				plan.m_missionSite.RemoveAsset (plan.m_currentAsset);

				// mark player's intel as stolen

				Player player = GameController.instance.game.playerList [0];

				foreach (Player.IntelSlot iSlot in player.intel) {

					if (iSlot.m_intelState == Player.IntelSlot.IntelState.Contested) {

						iSlot.m_intelState = Player.IntelSlot.IntelState.Stolen;
						break;
					}
				}

				// send notification that intel was stolen

				string title = "Intel Stolen";
				string message = "The Intel has been stolen by Agent " + plan.m_actorSlots[0].m_actor.m_actorName + "! If all Intel is stolen, the Agents will uncover the location of your Lair.";

				player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Missions, false, -1);

				GameController.instance.Notify (player, GameEvent.Player_IntelChanged);

			}
		}


	}
}
