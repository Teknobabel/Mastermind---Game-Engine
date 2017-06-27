using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour {

	public List<TurnPhase> m_turnPhases = new List<TurnPhase>();

	public List<Region> m_regions = new List<Region>();

	private bool m_gameOver = false;

	// Use this for initialization
	void Start () {
		StartCoroutine( DoGameLoop ());
	}

	private IEnumerator DoGameLoop ()
	{
		while (!m_gameOver) {

			foreach (TurnPhase tp in m_turnPhases) {

				yield return StartCoroutine (tp.StartTurnPhase ());
				yield return StartCoroutine (tp.DoTurnPhase ());
				yield return StartCoroutine (tp.EndTurnPhase ());
			}
		}

	}

	public void ExecuteAction (Action action)
	{
		action.ExecuteAction ();
	}
}
