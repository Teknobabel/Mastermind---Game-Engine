using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, ISubject {

	public static GameController instance;

	private List<IObserver> m_observers = new List<IObserver>();

	// UI and objects outside the game engine hook in here to listen for events and submit actions to the engine
	void Awake ()
	{
		instance = this;
	}
	// Use this for initialization
	void Start () {
		
	}

//	void Update () {
//
//		if (Input.GetKeyUp (KeyCode.Space)) {
//
//			GameEngine.instance.ProgressTurn ();
//		}
//	}

	public void ProcessAction (Action action)
	{
		action.ExecuteAction ();
	}

	public List<Region> GetWorld ()
	{
		List<Region> regionList = new List<Region> ();

		foreach (KeyValuePair<int, Region> pair in GameEngine.instance.game.regionList) {

			regionList.Add (pair.Value);
		}

		return regionList;
	}

	public List<Site.AssetSlot> GetAssets (int playerNum)
	{
		List<Site.AssetSlot> assets = new List<Site.AssetSlot> ();

		if (GameEngine.instance.game.playerList.ContainsKey (playerNum)) {

			Player player = GameEngine.instance.game.playerList [playerNum];

			foreach (Site.AssetSlot aSlot in player.assets) {

				assets.Add (aSlot);
			}
		} else {

			Debug.Log ("Player not found");
		}
			
		return assets;
	}
	
	// get hiring pool

	public List<Player.ActorSlot> GetHiringPool (int playerNum)
	{
		List<Player.ActorSlot> hiringPool = new List<Player.ActorSlot> ();


		if (GameEngine.instance.game.playerList.ContainsKey (playerNum)) {

			Player player = GameEngine.instance.game.playerList [playerNum];

			foreach (Player.ActorSlot aSlot in player.hiringPool.m_hireSlots) {

				hiringPool.Add (aSlot);
			}
		} else {

			Debug.Log ("Player not found");
		}

		return hiringPool;
	}

	public Actor GetActor (int actorID)
	{
		Actor actor = null;

		if (GameEngine.instance.game.henchmenList.ContainsKey (actorID)) {

			actor = GameEngine.instance.game.henchmenList [actorID];

		} else {

			Debug.Log ("Actor ID not found");
		}

		return actor;
	}

	// get all hired henchmen

	public List<Player.ActorSlot> GetHiredHenchmen (int playerNum)
	{
		List<Player.ActorSlot> hiredHenchmen = new List<Player.ActorSlot> ();

		if (GameEngine.instance.game.playerList.ContainsKey (playerNum)) {

			Player player = GameEngine.instance.game.playerList [playerNum];

			foreach (Player.ActorSlot aSlot in player.henchmenPool.m_henchmenSlots) {

				hiredHenchmen.Add (aSlot);
			}
		} else {

			Debug.Log ("Player not found");
		}

		return hiredHenchmen;
	}

	// get omega plan

	public Player.OmegaPlanSlot GetOmegaPlan (int playerNum)
	{
		Player.OmegaPlanSlot op = null;

		if (GameEngine.instance.game.playerList.ContainsKey (playerNum)) {

			Player player = GameEngine.instance.game.playerList [playerNum];
			op = player.omegaPlanSlot;

		} else {

			Debug.Log ("Player not found");
		}

		return op;
	}

	public int GetTurnNumber ()
	{
		return GameEngine.instance.game.currentTurn;
	}

	public Dictionary<int, List<NotificationCenter.Notification>> GetPlayerNotifications (int playerNum)
	{
		Dictionary<int, List<NotificationCenter.Notification>> notifications = new Dictionary<int, List<NotificationCenter.Notification>> ();

		if (GameEngine.instance.game.playerList.ContainsKey (playerNum)) {

			Player player = GameEngine.instance.game.playerList [playerNum];
			notifications = player.notifications.notifications;

		} else {

			Debug.Log ("Player not found");
		}

		return notifications;
	}

	public Dictionary<int, List<NotificationCenter.Notification>> GetHenchmenNotifications (int actorID)
	{
		Dictionary<int, List<NotificationCenter.Notification>> notifications = new Dictionary<int, List<NotificationCenter.Notification>> ();

		if (GameEngine.instance.game.henchmenList.ContainsKey (actorID)) {

			Actor actor = GameEngine.instance.game.henchmenList [actorID];
			notifications = actor.notifications.notifications;

		} else {

			Debug.Log ("Henchmen not found");
		}

		return notifications;
	}

	public Player.CommandPool GetCommandPool (int playerID)
	{
		Player.CommandPool cp = null;

		if (GameEngine.instance.game.playerList.ContainsKey (playerID)) {

			Player player = GameEngine.instance.game.playerList [playerID];
			cp = player.commandPool;

		} else {

			Debug.Log ("Player not found");
		}

		return cp;
	}

	public Lair GetLair (int playerID)
	{
		Lair lair = null;

		if (GameEngine.instance.game.playerList.ContainsKey (playerID)) {

			Player player = GameEngine.instance.game.playerList [playerID];
			lair = player.lair;

		} else {

			Debug.Log ("Player not found");
		}

		return lair;
	}

	public List<MissionPlan> GetMissions (int playerID)
	{
		List<MissionPlan> missions = new List<MissionPlan> ();

		if (GameEngine.instance.game.playerList.ContainsKey (playerID)) {

			Player player = GameEngine.instance.game.playerList [playerID];
			missions = player.currentMissions;

		} else {

			Debug.Log ("Player not found");
		}

		return missions;
	}

	public void CompileMission (MissionPlan plan)
	{
//		if (plan.m_missionSite == null || plan.m_currentMission == null) {
//
//			bool henchmenPresent = false;
//
//			foreach (Player.ActorSlot aSlot in plan.m_actorSlots) {
//
//				if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {
//
//					henchmenPresent = true;
//					break;
//				}
//			}
//
//			if (!henchmenPresent) {
//
//				plan.m_successChance = 0;
//				plan.m_requiredTraits.Clear ();
//				plan.m_matchingTraits.Clear ();
//				return;
//			}
//		}

		List<Trait> requiredTraits = new List<Trait> ();
		List<Trait> presentTraits = new List<Trait> ();
//		int successChance = 0;

		// get traits from mission

		foreach (Trait t in plan.m_currentMission.m_requiredTraits) {

			if (!requiredTraits.Contains (t)) {
				requiredTraits.Add (t);
			}
		}

		// get traits in response to site traits

		if (plan.m_missionSite != null && plan.m_currentMission.m_targetType != Mission.TargetType.Lair) {
			
			foreach (SiteTrait st in plan.m_missionSite.traits) {

				if (!requiredTraits.Contains (st.m_requiredTrait)) {
					requiredTraits.Add (st.m_requiredTrait);
				}
			}
		}

		// get traits in response to site alert level

		// get traits in response to selected asset, if applicable

		if (plan.m_currentAsset != null && plan.m_currentMission.m_targetType == Mission.TargetType.Asset) {

			foreach (Trait t in plan.m_currentAsset.m_asset.m_requiredTraits) {

				if (!requiredTraits.Contains (t)) {
					requiredTraits.Add (t);
				}
			}
		}

		// collect all traits from participating henchmen

		foreach (Player.ActorSlot aSlot in plan.m_actorSlots) {

			if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {

				foreach (Trait t in aSlot.m_actor.traits) {

					if (!presentTraits.Contains (t) && requiredTraits.Contains(t)) {

						presentTraits.Add (t);
					}
				}
			}
		}

		// see how many matching traits there are and calculate success chance
		// skills count for double

		float totalTraits = (float)requiredTraits.Count;
		float matchingTraits = (float)presentTraits.Count;
		float success = matchingTraits / totalTraits * 100;

		plan.m_requiredTraits = requiredTraits;
		plan.m_matchingTraits = presentTraits;
		plan.m_successChance = (int)success;
	}

	public void AddObserver (IObserver observer)
	{
		m_observers.Add (observer);
	}

	public void RemoveObserver (IObserver observer)
	{
		if (m_observers.Contains (observer)) {

			m_observers.Remove (observer);
		}
	}

	public void Notify (ISubject subject, GameEvent thisGameEvent)
	{
		foreach (IObserver o in m_observers) {

			o.OnNotify (subject, thisGameEvent);
		}
	}



}
