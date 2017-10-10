using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_StartNewGame : Action {

	public override void ExecuteAction ()
	{
		Debug.Log ("Starting new game!");

		// create game

		Game newGame = new Game ();
		GameEngine.instance.AddNewGame (newGame);

		// choose director

		Director director = GameEngine.instance.m_directorList[Random.Range(0, GameEngine.instance.m_directorList.Length)];
		newGame.AddDirector (director);

		// create player

		Player newPlayer = new Player ();
		newPlayer.id = 0;
		newGame.AddPlayer (newPlayer);

		// create command pool

		Player.CommandPool commandPool = new Player.CommandPool();
		commandPool.m_basePool = director.m_startingCommandPool;
		newPlayer.AddCommandPool (commandPool);

		// create lair

//		Lair newLair = new Lair ();
//		newLair.Initialize (director.m_startingFloorSlots);
//		newPlayer.AddLair (newLair);

		// add any starting floors

//		foreach (Floor f in director.m_startingFloors) {
//
//			Action_BuildNewFloor buildFloor = new Action_BuildNewFloor ();
//			buildFloor.m_player = newPlayer;
//			buildFloor.m_floor = f;
//			GameController.instance.ProcessAction (buildFloor);
//		}

		Lair newLair = (Lair)Object.Instantiate (director.m_startingLair);
		newPlayer.AddLair (newLair);
		newLair.Initialize ();

		// add any starting assets

		foreach (Asset a in director.m_startingAssets) {
			Action_GainAsset gainAsset = new Action_GainAsset ();
			gainAsset.m_player = newPlayer;
			gainAsset.m_asset = a;

			GameController.instance.ProcessAction (gainAsset);
		}

		// pick omega plan

		Action_UnlockOmegaPlan unlockOP = new Action_UnlockOmegaPlan ();
		unlockOP.m_player = newPlayer;

		if (director.m_startingOmegaPlan != null) {
			unlockOP.m_omegaPlans.Add (director.m_startingOmegaPlan);
		} else {
			unlockOP.m_omegaPlans = GameEngine.instance.m_omegaPlans;
		}

		GameController.instance.ProcessAction (unlockOP);

		// gather any mandatory assets from omega plan

		List<Asset> mandatoryAssets = new List<Asset> ();

		foreach (OmegaPlan.Phase thisPhase in newPlayer.omegaPlanSlot.m_omegaPlan.phases) {

			foreach (OmegaPlan.OPGoal thisGoal in thisPhase.m_goals) {

				if (thisGoal.m_mandatoryAssets.Length > 0) {

					foreach (Asset a in thisGoal.m_mandatoryAssets) {

						mandatoryAssets.Add (a);
					}
				}
			}
		}

		// instantiate regions

		int siteID = 0;

		for (int i = 0; i < GameEngine.instance.m_regions.Count; i++) {

			Region newRegion = (Region)Object.Instantiate (GameEngine.instance.m_regions [i]);
			newRegion.id = i;

			for (int j = 0; j < newRegion.m_startingSites.Count; j++) {

				//				Site s = newRegion.m_sites [j];

				Site s = (Site)Object.Instantiate (newRegion.m_startingSites [j]);
				s.id = siteID;
				siteID++;
				s.regionID = newRegion.id;
				s.Initialize ();
				newRegion.AddSite (s);
			}

			newGame.AddRegion (newRegion);
		}

		// disperse mandatory assets from OP to sites

		if (mandatoryAssets.Count > 0) {

			while (mandatoryAssets.Count > 0) {

				Asset a = mandatoryAssets [0];
				mandatoryAssets.RemoveAt (0);

				// gather a list of valid regions based on asset rank vs site max alert level

				List<Site> validSites = new List<Site> ();

				foreach (KeyValuePair<int, Site> pair in newGame.siteList) {

					if (pair.Value.m_maxAlertLevel >= a.m_rank) {

						validSites.Add (pair.Value);
					}
				}

				if (validSites.Count > 0) {

					Site s = validSites [Random.Range (0, validSites.Count)];
					s.AddAsset (a);

				} else {

					Debug.Log ("No valid sites found for asset: " + a.m_name + " rank: " + a.m_rank);
				}
			}
		}


		// initialize hiring pool

		Player.HiringPool hiringPool = new Player.HiringPool ();

		for (int i = 0; i < director.m_startingHireSlots; i++) {

			Player.ActorSlot aSlot = new Player.ActorSlot ();
			aSlot.m_state = Player.ActorSlot.ActorSlotState.Empty;
			hiringPool.m_hireSlots.Add (aSlot);
		}

		// instantiate henchmen

		List<int> startingHenchmen = new List<int> ();
		List<int> startingHireableHenchmen = new List<int> ();

		for (int i = 0; i < GameEngine.instance.m_henchmenList.Length; i++) {

			Actor a = GameEngine.instance.m_henchmenList [i];
			Actor newHenchmen = (Actor)Object.Instantiate (a);
			newHenchmen.id = i;
			newHenchmen.Initialize ();
			newGame.AddHenchmen (newHenchmen);
			hiringPool.m_availableHenchmen.Add (newHenchmen);

			foreach (Actor thisA in director.m_startingHenchmen) {

				if (thisA == a) {
					startingHenchmen.Add (newHenchmen.id);
					break;
				}
			}

			foreach (Actor thisA in director.m_startingHireableHenchmen) {

				if (thisA == a) {
					startingHireableHenchmen.Add (newHenchmen.id);
					break;
				}
			}
		}

		newPlayer.AddHiringPool (hiringPool);

		// instantiate henchmen pool

		Player.HenchmenPool henchmenPool = new Player.HenchmenPool ();

		for (int i = 0; i < director.m_startingHenchmenSlots; i++) {

			Player.ActorSlot aSlot = new Player.ActorSlot ();
			aSlot.m_state = Player.ActorSlot.ActorSlotState.Empty;
			henchmenPool.m_henchmenSlots.Add (aSlot);
		}

		newPlayer.AddHenchmenPool (henchmenPool);


		// add any starting henchmen from director

		foreach (int thisID in startingHenchmen) {

			Action_HireAgent hireHenchmen = new Action_HireAgent ();
			hireHenchmen.m_henchmenID = thisID;
			hireHenchmen.m_playerNumber = newPlayer.id;
			GameController.instance.ProcessAction (hireHenchmen);

		}

		// add any starting hireable henchmen from director

		foreach (int thisID in startingHireableHenchmen) {

			Actor henchmen = newGame.henchmenList [thisID];
			Action_MakeHireable makeHireable = new Action_MakeHireable ();
			makeHireable.m_player = newPlayer;
			makeHireable.m_henchmen = henchmen;
			GameController.instance.ProcessAction (makeHireable);
		}

		// populate empty hiring slots

		for (int i = 0; i < hiringPool.m_hireSlots.Count; i++) {

			Player.ActorSlot aSlot = hiringPool.m_hireSlots [i];

			if (aSlot.m_state == Player.ActorSlot.ActorSlotState.Empty) {

				Actor henchmen = newPlayer.hiringPool.GetHenchmenToHire (newPlayer.infamy);

				if (henchmen != null) {
					Action_MakeHireable addToHiringPool = new Action_MakeHireable ();
					addToHiringPool.m_player = newPlayer;
					addToHiringPool.m_henchmen = henchmen;
					GameController.instance.ProcessAction (addToHiringPool);
				}
			}
		}


		// distribute assets from asset bank

		foreach (Director.AssetBank aBank in director.m_assetBank) {

			int maxRank1SiteAssets = 2;
			int maxRank2SiteAssets = 3;
			int maxRank3SiteAssets = 4;
			int maxRank4SiteAssets = 5;
			int maxRank5SiteAssets = 6;

			foreach (Asset asset in aBank.m_assets) {

				List<Site> validSites = new List<Site> ();

				foreach (KeyValuePair<int, Site> pair in newGame.siteList) {

					// check if site has enough room for another asset

					bool hasRoom = false;
					int numTimes = 0;

					if (pair.Value.assets.Count == 0) {

						numTimes += 3;
					}

					switch (pair.Value.m_maxAlertLevel) {

					case 1:

						if (pair.Value.assets.Count < maxRank1SiteAssets) {

							hasRoom = true;
						}
						break;
					case 2:

						if (pair.Value.assets.Count < maxRank2SiteAssets) {

							hasRoom = true;
						}
						break;
					case 3:

						if (pair.Value.assets.Count < maxRank3SiteAssets) {

							hasRoom = true;
						}
						break;
					case 4:

						if (pair.Value.assets.Count < maxRank4SiteAssets) {

							hasRoom = true;
						}
						break;
					case 5:

						if (pair.Value.assets.Count < maxRank5SiteAssets) {

							hasRoom = true;
						}
						break;

					}

					if (hasRoom)
					{

						// more likely if site is of a matching type

						if (asset.m_preferedSiteType == pair.Value.m_type || asset.m_preferedSiteType == Site.Type.None) {

							numTimes += 10;
						}

						// more likely if site is of an appropriate level

						if (asset.m_rank <= pair.Value.m_maxAlertLevel) {

							numTimes += 5;
						}
					}

					for (int i = 0; i < numTimes; i++) {

						validSites.Add (pair.Value);
					}

				}

				if (validSites.Count > 0) {

					int r = Random.Range (0, validSites.Count);

					Site thisSite = validSites [r];
					thisSite.AddAsset (asset);


				} else {

					Debug.Log ("No valid site found for asset");
				}
			}
		}

	}
}
