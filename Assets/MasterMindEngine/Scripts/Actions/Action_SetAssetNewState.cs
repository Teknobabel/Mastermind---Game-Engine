using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_SetAssetNewState : Action {

	public Site.AssetSlot m_assetSlot;
	public bool m_newState = false;

	public override void ExecuteAction ()
	{
		m_assetSlot.m_new = m_newState;

		Player player = GameEngine.instance.game.playerList [0];
		GameController.instance.Notify (player, GameEvent.Player_AssetsChanged);
	}
}
