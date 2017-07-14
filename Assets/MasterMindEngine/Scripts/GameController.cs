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

	public OmegaPlan GetOmegaPlan (int playerNum)
	{
		OmegaPlan op = null;

		if (GameEngine.instance.game.playerList.ContainsKey (playerNum)) {

			Player player = GameEngine.instance.game.playerList [playerNum];
			op = player.omegaPlan;

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
