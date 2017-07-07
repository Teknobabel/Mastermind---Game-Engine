﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_StartNewGame : Action {

	public override void ExecuteAction ()
	{
		Debug.Log ("Starting new game!");

		// choose director

		Director director = GameEngine.instance.m_directorList[Random.Range(0, GameEngine.instance.m_directorList.Length)];

		// create game

		Game newGame = new Game ();

		// create player

		Player newPlayer = new Player ();
		newPlayer.id = 0;
		newGame.AddPlayer (newPlayer);

		// create command pool

		Player.CommandPool commandPool = new Player.CommandPool();
		commandPool.m_basePool = director.m_startingCommandPool;
		newPlayer.AddCommandPool (commandPool);

		// create lair

		Lair newLair = new Lair ();
		newPlayer.AddLair (newLair);

		// add any starting floors

		foreach (Floor f in director.m_startingFloors) {

			Action_BuildNewFloor buildFloor = new Action_BuildNewFloor ();
			buildFloor.m_player = newPlayer;
			buildFloor.m_floor = f;
			GameController.instance.ProcessAction (buildFloor);
		}
			
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

		// instantiate regions

		int siteID = 0;

		for (int i = 0; i < GameEngine.instance.m_regions.Count; i++) {
		
			Region newRegion = (Region)Object.Instantiate (GameEngine.instance.m_regions [i]);
			newRegion.id = i;

			for (int j = 0; j < newRegion.m_sites.Count; j++) {

				Site s = newRegion.m_sites [j];
				s.id = siteID;
				siteID++;
			}

			newGame.AddRegion (newRegion);
		}

		// add mandatory assets from OP to regions



		// initialize hiring pool

		Player.HiringPool hiringPool = new Player.HiringPool ();

		for (int i = 0; i < director.m_startingHireSlots; i++) {

			Player.ActorSlot aSlot = new Player.ActorSlot ();
			aSlot.m_state = Player.ActorSlot.ActorSlotState.Empty;
			hiringPool.m_hireSlots.Add (aSlot);
		}

		// instantiate henchmen

		for (int i = 0; i < GameEngine.instance.m_henchmenList.Length; i++) {

			Actor a = GameEngine.instance.m_henchmenList [i];
			Actor newHenchmen = (Actor)Object.Instantiate (a);
			newHenchmen.id = i;
			newGame.AddHenchmen (newHenchmen);
			hiringPool.m_availableHenchmen.Add (newHenchmen);
		}

		newPlayer.AddHiringPool (hiringPool);

		// populate empty hiring slots

		for (int i = 0; i < hiringPool.m_hireSlots.Count; i++) {
			
			Action_MakeHireable makeHireable = new Action_MakeHireable ();
			makeHireable.m_player = newPlayer;

			int rand = Random.Range (0, hiringPool.m_availableHenchmen.Count);
			Actor a = hiringPool.m_availableHenchmen [rand];
			makeHireable.m_henchmen = a;

			GameController.instance.ProcessAction (makeHireable);
		}

		// instantiate henchmen pool

		Player.HenchmenPool henchmenPool = new Player.HenchmenPool ();

		for (int i = 0; i < director.m_startingHenchmenSlots; i++) {

			Player.ActorSlot aSlot = new Player.ActorSlot ();
			aSlot.m_state = Player.ActorSlot.ActorSlotState.Empty;
			henchmenPool.m_henchmenSlots.Add (aSlot);
		}

		newPlayer.AddHenchmenPool (henchmenPool);


		// add any starting henchmen from director


		GameEngine.instance.AddNewGame (newGame);
	}
}
