using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool {

	public List<Effect> m_effectPool = new List<Effect>();

	public void AddEffect (Effect e)
	{
		m_effectPool.Add (e);
	}

	public int GetValue (Effect.EffectType type)
	{
		int value = 0;

		foreach (Effect e in m_effectPool) {

			if (e.m_effectType == type) {

				value += e.GetValue ();
			}
		}

		return value;
	}

	public void UpdateDuration ()
	{
		List<Effect> oldEffects = new List<Effect> ();

		foreach (Effect e in m_effectPool) {

			e.m_duration--;

			if (e.m_duration <= 0) {

				oldEffects.Add (e);
			}
		}

		while (oldEffects.Count > 0) {

			Effect e = oldEffects [0];
			oldEffects.RemoveAt (0);

			m_effectPool.Remove (e);
		}
	}
}
