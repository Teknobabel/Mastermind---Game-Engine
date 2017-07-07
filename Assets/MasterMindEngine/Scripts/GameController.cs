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
