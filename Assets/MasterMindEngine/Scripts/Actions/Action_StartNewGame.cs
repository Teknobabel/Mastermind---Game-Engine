using System.Collections;
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

		// add any starting assets

		// pick omega plan

		ScriptableObject op = (ScriptableObject)GameEngine.instance.m_omegaPlans[Random.Range(0, GameEngine.instance.m_omegaPlans.Count)];
		OmegaPlan newOmegaPlan = (OmegaPlan)Object.Instantiate (op);
		newPlayer.AddOmegaPlan (newOmegaPlan);

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

		// populate empty hiring slots

		foreach (Player.ActorSlot aSlot in hiringPool.m_hireSlots) {

			if (hiringPool.m_availableHenchmen.Count > 0) {

				int rand = Random.Range (0, hiringPool.m_availableHenchmen.Count);

				Actor a = hiringPool.m_availableHenchmen [rand];
				hiringPool.m_availableHenchmen.RemoveAt (rand);
				aSlot.SetHenchmen (a);
			}
		}

		newPlayer.AddHiringPool (hiringPool);


		GameEngine.instance.AddNewGame (newGame);
	}
}
