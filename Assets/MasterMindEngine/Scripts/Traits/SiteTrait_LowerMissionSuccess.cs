using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteTrait_LowerMissionSuccess : SiteTrait {

	public int amount = -10;

	private int m_effectID = -1;

	public override void TraitAdded (Site site)
	{
		// create effect and add to site, storing the effect ID for removal

		Effect newEffect = new Effect ();
		newEffect.m_value = -10;
		newEffect.m_effectType = Effect.EffectType.SuccessChanceModifier;
		newEffect.m_effectName = m_name;

		m_effectID = site.effectPool.AddEffect (newEffect);
	}

	public override void TraitRemoved (Site site)
	{
		if (m_effectID != -1) {

			site.effectPool.RemoveEffect (m_effectID);
		}
	}

	public override string GetDescription ()
	{
		string s = "Missions run at this location receive a " + amount.ToString () + "% penalty to success chance.";
		return s;
	}
}
