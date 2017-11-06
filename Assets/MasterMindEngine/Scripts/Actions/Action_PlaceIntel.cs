using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_PlaceIntel : Action {

	public int m_playerID = -1;

	public override void ExecuteAction ()
	{
		Player player = GameEngine.instance.game.playerList [m_playerID];

		Player.IntelSlot slot = null;
		int numIntelStolen = 0;

		// make sure there is a valid intel slot

		foreach (Player.IntelSlot iSlot in player.intel) {

			if (iSlot.m_intelState == Player.IntelSlot.IntelState.Owned && slot == null) {

				slot = iSlot;
			} else if (iSlot.m_intelState == Player.IntelSlot.IntelState.Stolen) {

				numIntelStolen++;
			}
		}

		// mark it as contested

		if (slot != null)
		{
			List<Site> validSites = new List<Site> ();

			foreach (KeyValuePair<int, Site> pair in GameController.instance.game.siteList)
			{
				// early intels are placed in lower rank sites

				// later intels are placed in lower rank sites

				if ((numIntelStolen > 3 && pair.Value.m_maxAlertLevel > 3) || (numIntelStolen <= 3 && pair.Value.m_maxAlertLevel <= 3)) {

					validSites.Add (pair.Value);
				}
			}

			// place in a site as revealed asset

			if (validSites.Count > 0) {

				slot.m_intelState = Player.IntelSlot.IntelState.Contested;

				Site s = validSites[Random.Range(0, validSites.Count)];

				Asset intelAsset = new Asset ();
				intelAsset.m_name = "Intel";
				intelAsset.m_rank = 0;

				Site.AssetSlot aSlot = s.AddAsset (intelAsset);
				aSlot.m_state = Site.AssetSlot.State.Revealed;

				// add notification that intel has appeared

				string title = "Intel Uncovered!";
				string message = "Intel has appeared in " + s.m_siteName + ", recover it quickly!";

				player.notifications.AddNotification (GameController.instance.GetTurnNumber(), title, message, EventLocation.Missions, false, -1);

				GameController.instance.Notify (player, GameEvent.Player_IntelChanged);
			}
		}
	}
}
