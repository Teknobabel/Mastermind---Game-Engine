﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour {
	public static GameEngine instance;

	public bool m_forceMissionSuccess = false;

	public List<TurnPhase> m_turnPhases = new List<TurnPhase>();

	public List<Region> m_regions = new List<Region>();

	public List<OmegaPlan> m_omegaPlans = new List<OmegaPlan> ();

	public Actor[] m_henchmenList;

	public Actor[] m_agentList;

	public Director[] m_directorList;

	public StatusTrait[] m_statusTraits;

	private Game m_game;

//	private bool m_gameOver = false;

	private int m_currentTurnPhase = 0;

	void Awake ()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {

//		Action_StartNewGame newGameAction = new Action_StartNewGame ();
//		GameController.instance.ProcessAction (newGameAction);
//
//		ProgressTurn ();

//		StartCoroutine( DoGameLoop ());
	}

//	private IEnumerator DoGameLoop ()
//	{
//		while (!m_gameOver) {
//
//			foreach (TurnPhase tp in m_turnPhases) {
//
//				yield return StartCoroutine (tp.StartTurnPhase ());
//				yield return StartCoroutine (tp.DoTurnPhase ());
//				yield return StartCoroutine (tp.EndTurnPhase ());
//			}
//		}
//	}

	public void AddNewGame (Game game)
	{
		m_game = game;
	}

	public void ProgressTurn ()
	{
		bool proceedToNextPhase = true;

		while (proceedToNextPhase) {

			TurnPhase tPhase = m_turnPhases [m_currentTurnPhase];
			tPhase.DoTurnPhase ();

			m_currentTurnPhase++;

			if (m_currentTurnPhase >= m_turnPhases.Count) {

				m_currentTurnPhase = 0;
			}

			if (!tPhase.m_proceedToNextPhase) {

				proceedToNextPhase = false;
			}

		}

//		m_currentTurnPhase++;
//
//		if (m_currentTurnPhase >= m_turnPhases.Count) {
//
//			m_currentTurnPhase = 0;
//		}
//
//		TurnPhase newPhase = m_turnPhases [m_currentTurnPhase];
//		newPhase.DoTurnPhase ();
	}

//	public void ExecuteAction (Action action)
//	{
//		action.ExecuteAction ();
//	}

	public Game game {get{ return m_game; }}
}
