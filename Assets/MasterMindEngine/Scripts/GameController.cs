using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController instance;

	// UI and objects outside the game engine hook in here to listen for events and submit actions to the engine
	void Awake ()
	{
		instance = this;
	}
	// Use this for initialization
	void Start () {
		
	}

	public void ProcessAction (Action action)
	{
		action.ExecuteAction ();
	}
	
	// Update is called once per frame
//	void Update () {
//		
//	}
}
