using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_GainEffect : Mission {

	public enum EffectTarget
	{
		None,
		Player,
		Floor,
	}

	public Effect m_effect;
	public EffectTarget m_effectTarget = EffectTarget.None;

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		if (plan.m_result == MissionPlan.Result.Success && m_effectTarget != null) {

			Player player = GameEngine.instance.game.playerList [0];

			Action_GainEffect gainEffect = new Action_GainEffect ();
			gainEffect.m_missionID = plan.m_missionID;
			gainEffect.m_effect = m_effect;
			gainEffect.m_playerID = 0;

			switch (m_effectTarget) {

			case EffectTarget.Player:
				
				gainEffect.m_effectTarget = player;

				break;

			case EffectTarget.Floor:
				
				if (plan.m_floorSlot != null) {
					gainEffect.m_effectTarget = plan.m_floorSlot.m_floor;
				}

				break;
			}

			GameController.instance.ProcessAction (gainEffect);
		}
	}
}
