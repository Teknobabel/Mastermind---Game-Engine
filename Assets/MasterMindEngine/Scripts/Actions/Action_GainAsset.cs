﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_GainAsset : Action {

	public Player m_player = null;

	public Asset m_asset = null;

	public override void ExecuteAction ()
	{
		Debug.Log ("Player gaining asset: " + m_asset.m_name);
		m_player.AddAsset (m_asset);
	}
}