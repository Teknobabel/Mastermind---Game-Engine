using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Effect : ScriptableObject {

	public enum EffectType
	{
		None,
		SuccessChanceModifier,
	}

	public EffectType m_effectType = EffectType.None;
	public string m_effectName = "Null";
	public int m_duration = 1;

	public int m_value = 1;
//	public virtual void Initialize ()
//	{
//
//	}
//
	public virtual int GetValue ()
	{
		return m_value;
	}
}
