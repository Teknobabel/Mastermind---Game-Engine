using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour {
	public static GameEngine instance;

	public List<TurnPhase> m_turnPhases = new List<TurnPhase>();

	public List<Region> m_regions = new List<Region>();

	public List<OmegaPlan> m_omegaPlans = new List<OmegaPlan> ();

	public Actor[] m_henchmenList;

	public Director[] m_directorList;

	private Game m_game;

	private bool m_gameOver = false;

	void Awake ()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {

		Action_StartNewGame newGameAction = ScriptableObject.CreateInstance<Action_StartNewGame> ();

		GameController.instance.ProcessAction (newGameAction);

//		StartCoroutine( DoGameLoop ());
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

	public void AddNewGame (Game game)
	{
		m_game = game;
	}

//	public void ExecuteAction (Action action)
//	{
//		action.ExecuteAction ();
//	}

	public Game game {get{ return m_game; }}
}
