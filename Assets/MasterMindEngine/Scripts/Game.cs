﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game  {

	private Dictionary<int, Player> m_playerList = new Dictionary<int, Player>();

	private Dictionary<int, AgentPlayer> m_agentPlayerList = new Dictionary<int, AgentPlayer>();

	private Dictionary<int, Region> m_regionList = new Dictionary<int, Region>();

	private Dictionary<int, Site> m_siteList = new Dictionary<int, Site>();

	private Dictionary<int, Actor> m_henchmenList = new Dictionary<int, Actor>();

	private Dictionary<int, Actor> m_agentList = new Dictionary<int, Actor>();

	private Director m_director;

	private int m_currentTurn = 0;

	private int m_currentID = -1;

	public void AddDirector (Director director)
	{
		m_director = director;
	}

	public void AddPlayer (Player player)
	{
		if (!m_playerList.ContainsKey (player.id)) {

			m_playerList.Add (player.id, player);

		} else {

			Debug.Log ("Player with this player number already exists!");
		}
	}

	public void AddAgentPlayer (AgentPlayer agentPlayer)
	{
		if (!m_agentPlayerList.ContainsKey (agentPlayer.id)) {

			m_agentPlayerList.Add (agentPlayer.id, agentPlayer);

		} else {

			Debug.Log ("Agent Player with this player number already exists!");
		}
	}

	public void AddRegion (Region newRegion)
	{
		if (!m_regionList.ContainsKey (newRegion.id)) {

			m_regionList.Add (newRegion.id, newRegion);

			foreach (Site newSite in newRegion.sites) {

				AddSite (newSite);
			}

		} else {

			Debug.Log ("Region with this id already exists!");
		}
	}

	public void AddSite (Site newSite)
	{
		if (!m_siteList.ContainsKey (newSite.id)) {

			m_siteList.Add (newSite.id, newSite);

		} else {

			Debug.Log ("Site with this id already exists!");
		}
	}

	public void AddHenchmen (Actor newHenchmen)
	{
		if (!m_henchmenList.ContainsKey (newHenchmen.id)) {

			m_henchmenList.Add (newHenchmen.id, newHenchmen);

		} else {

			Debug.Log ("Henchmen with this id already exists!");
		}
	}

	public void AddAgent (Actor newAgent)
	{
		if (!m_agentList.ContainsKey (newAgent.id)) {

			m_agentList.Add (newAgent.id, newAgent);

		} else {

			Debug.Log ("Agent with this id already exists!");
		}
	}

	public void IncrementTurn ()
	{
		m_currentTurn++;
	}

	public int GetID ()
	{
		m_currentID++;
		return m_currentID;
	}

	public Dictionary<int, Player> playerList {get{ return m_playerList; }}
	public Dictionary<int, AgentPlayer> agentPlayerList {get{ return m_agentPlayerList; }}
	public Dictionary<int, Actor> henchmenList {get{ return m_henchmenList; }}
	public Dictionary<int, Actor> agentList {get{ return m_agentList; }}
	public Dictionary<int, Region> regionList {get{ return m_regionList; }}
	public Dictionary<int, Site> siteList {get{ return m_siteList; }}
	public int currentTurn {get{return m_currentTurn;}}
	public Director director {get{ return m_director;}}
}
