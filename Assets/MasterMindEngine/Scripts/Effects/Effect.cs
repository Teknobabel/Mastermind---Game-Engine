using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect {

	public enum EffectType
	{
		None,
		SuccessChanceModifier,
	}

	public EffectType m_effectType = EffectType.None;

	public int m_duration = 1;

	public virtual void Initialize ()
	{

	}

	public virtual int GetValue ()
	{
		return 0;
	}
}
