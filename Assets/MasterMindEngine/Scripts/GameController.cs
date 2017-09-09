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

	public Site GetSite (int siteID)
	{
		Site s = null;

		if (GameEngine.instance.game.siteList.ContainsKey (siteID)) {

			s = GameEngine.instance.game.siteList [siteID];

		} else {

			Debug.Log ("Site not found");
		}

		return s;
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

	public int GetNumAssetSlots (int playerNum)
	{
		if (GameEngine.instance.game.playerList.ContainsKey (playerNum)) {

			Player player = GameEngine.instance.game.playerList [playerNum];

			return player.NumAssetSlots ();

		} else {

			Debug.Log ("Player not found");
		}

		return 0;
	}

	public int GetNumFloorSlots (int playerNum)
	{
		if (GameEngine.instance.game.playerList.ContainsKey (playerNum)) {

			Player player = GameEngine.instance.game.playerList [playerNum];

			return player.NumFloorSlots ();

		} else {

			Debug.Log ("Player not found");
		}

		return 0;
	}

	public int GetAssetUpkeep (int playerNum)
	{
		int upkeep = 0;

		int numAssetSlots = GameController.instance.GetNumAssetSlots (playerNum);
		int numAssets = 0;
		List<Site.AssetSlot> assets = GameController.instance.GetAssets (playerNum);

		foreach (Site.AssetSlot aSlot in assets) {

			if (aSlot.m_state != Site.AssetSlot.State.None) {
				numAssets++;
			}
		}

		if (numAssets > numAssetSlots) {

			upkeep = numAssets - numAssetSlots;
		}

		return upkeep;
	}

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

	public List<NotificationCenter.Notification> GetMissionNotifications (int playerNum, int missionID)
	{
		List<NotificationCenter.Notification> notifications = new List<NotificationCenter.Notification> ();

		if (GameEngine.instance.game.playerList.ContainsKey (playerNum)) {

			Player player = GameEngine.instance.game.playerList [playerNum];

			if (player.notifications.notificationsByMissionID.ContainsKey (missionID)) {
				
				notifications = player.notifications.notificationsByMissionID [missionID];

			} else {
				Debug.Log ("Mission ID not found");
			}

		} else {

			Debug.Log ("Player not found");
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

		if (plan.m_currentMission == null) {

			return;
		}

		List<Trait> requiredTraits = new List<Trait> ();
		List<Trait> presentTraits = new List<Trait> ();

		List<Asset> requiredAssets = new List<Asset> ();

		int skillWeight = 3; // higher=more weight in determining overal mission success % 

		int numTraitsModified = 0;
		int numAssetsModified = 0;
		float successModifier = 0;
		//		int successChance = 0;

		// get traits from mission

		foreach (Trait t in plan.m_currentMission.m_requiredTraits) {

			if (!requiredTraits.Contains (t)) {
				requiredTraits.Add (t);

				if (t.m_type == Trait.Type.Skill) {
					numTraitsModified += skillWeight;
				} else {
					numTraitsModified++;
				}
			}
		}

		// get assets from mission

		foreach (Asset a in plan.m_currentMission.m_requiredAssets) {

			requiredAssets.Add (a);
			numAssetsModified += a.m_rank;
		}

		// get traits in response to site traits

		if (plan.m_missionSite != null && plan.m_currentMission.m_targetType != Mission.TargetType.Lair) {

			foreach (SiteTrait st in plan.m_missionSite.traits) {

				if (!requiredTraits.Contains (st.m_requiredTrait)) {

					requiredTraits.Add (st.m_requiredTrait);

					if (st.m_requiredTrait.m_type == Trait.Type.Skill) {
						numTraitsModified += skillWeight;
					} else {
						numTraitsModified++;
					}
				}
			}
		}

		// get traits in response to site alert level

		if (plan.m_missionSite != null && plan.m_missionSite.currentAlertLevel > 0 && plan.m_currentMission.m_targetType != Mission.TargetType.Lair) {

			foreach (Director.AlertData aData in GameEngine.instance.game.director.m_alertData) {

				if (aData.m_siteType == plan.m_missionSite.m_type) {

					for (int i = 0; i < aData.m_traitList.Length; i++) {

						if (i < plan.m_missionSite.currentAlertLevel) {

							Trait t = aData.m_traitList [i];

							if (!requiredTraits.Contains (t)) {
								requiredTraits.Add (t);

								if (t.m_type == Trait.Type.Skill) {
									numTraitsModified += skillWeight;
								} else {
									numTraitsModified++;
								}
							}
						}
					}

					break;
				}
			}

		}

		// get traits in response to selected asset, if applicable

		if (plan.m_currentAsset != null && plan.m_currentMission.m_targetType == Mission.TargetType.Asset) {

			foreach (Trait t in plan.m_currentAsset.m_asset.m_requiredTraits) {

				if (!requiredTraits.Contains (t)) {

					requiredTraits.Add (t);

					if (t.m_type == Trait.Type.Skill) {
						numTraitsModified += skillWeight;
					} else {
						numTraitsModified++;
					}
				}
			}
		}

		// collect all traits from participating henchmen

		int numTraitsPresentModified = 0;
		int numAssetsPresentModified = 0;

		// make sure all participating henchmen are free

		for (int i = 0; i < plan.m_actorSlots.Count; i++) {

			Player.ActorSlot aSlot = plan.m_actorSlots [i];

			if (aSlot.m_state == Player.ActorSlot.ActorSlotState.OnMission) {

				plan.m_actorSlots.RemoveAt (i);
				i = 0;
			}
		}

		foreach (Player.ActorSlot aSlot in plan.m_actorSlots) {

			if (aSlot.m_state != Player.ActorSlot.ActorSlotState.Empty) {

				// check status

				successModifier += (float)aSlot.m_actor.m_status.m_successModifier;

				foreach (Trait t in aSlot.m_actor.traits) {

					if (!presentTraits.Contains (t) && requiredTraits.Contains(t)) {

						presentTraits.Add (t);

						if (t.m_type == Trait.Type.Skill) {
							numTraitsPresentModified += skillWeight;
						} else {
							numTraitsPresentModified++;
						}
					}
				}
			}
		}

		// check for matching assets

		List<Site.AssetSlot> playerAssets = GetAssets (0);
		List<Site.AssetSlot> matchingAssets = new List<Site.AssetSlot> ();

		foreach (Asset a in requiredAssets) {

			for (int i=0; i < playerAssets.Count; i++)
			{
				Site.AssetSlot aSlot = playerAssets [i];

				if (aSlot.m_state != Site.AssetSlot.State.None && aSlot.m_state != Site.AssetSlot.State.InUse && aSlot.m_asset == a) {

					numAssetsPresentModified += a.m_rank;
					matchingAssets.Add (aSlot);
//					aSlot.m_state = Site.AssetSlot.State.InUse;
					playerAssets.RemoveAt (i);
					i = 99;
					break;
				}
			}
		}



		// see how many matching traits and assets there are and calculate success chance

		//		float totalTraits = (float)requiredTraits.Count;
		//		float matchingTraits = (float)presentTraits.Count;
		float total = (float)numTraitsModified + (float)numAssetsModified;
		float matching = (float)numTraitsPresentModified + (float)numAssetsPresentModified;
		float success = Mathf.Clamp( (matching / total * 100) + successModifier, 0.0f, 100.0f);

		plan.m_requiredAssets = requiredAssets;
		plan.m_requiredTraits = requiredTraits;
		plan.m_matchingTraits = presentTraits;
		plan.m_linkedPlayerAssets = matchingAssets;
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

	public Game game {get{ return GameEngine.instance.game; }}

}
