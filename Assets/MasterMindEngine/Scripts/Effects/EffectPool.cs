using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool {

	public class EffectSlot
	{
		public Effect m_effect;
		public int m_currentDuration = 1,
		m_effectID = -1;
		public bool m_new = true;

	}

	public List<EffectSlot> m_effectPool = new List<EffectSlot>();

	public int AddEffect (Effect e)
	{
		Debug.Log ("Gaining new effect: " + e.m_effectName);

		EffectSlot eSlot = new EffectSlot ();
		eSlot.m_effect = e;
		eSlot.m_currentDuration = e.m_duration;
		eSlot.m_effectID = GameController.instance.game.GetID ();
		m_effectPool.Add (eSlot);

		return eSlot.m_effectID;
	}

	public void RemoveEffect (int effectID)
	{
		for (int i = 0; i < m_effectPool.Count; i++) {

			EffectSlot eSlot = m_effectPool [i];
			if (eSlot.m_effectID == effectID) {

				m_effectPool.RemoveAt (i);
				return;
			}
		}
	}

	public List<EffectSlot> GetEffects (Effect.EffectType type)
	{
		List<EffectSlot> effects = new List<EffectSlot> ();

		foreach (EffectSlot eSlot in m_effectPool) {

			if (eSlot.m_effect.m_effectType == type) {

				effects.Add (eSlot);
			}
		}

		return effects;
	}

	public int GetValue (Effect.EffectType type)
	{
		int value = 0;

		foreach (EffectSlot eSlot in m_effectPool) {

			if (eSlot.m_effect.m_effectType == type) {

				value += eSlot.m_effect.GetValue ();
			}
		}

		return value;
	}

	public void UpdateDuration ()
	{
		List<EffectSlot> oldEffects = new List<EffectSlot> ();

		foreach (EffectSlot eSlot in m_effectPool) {

			if (eSlot.m_new) {

				eSlot.m_new = false;

			} else {
				
				eSlot.m_currentDuration--;

				if (eSlot.m_currentDuration <= 0) {

					oldEffects.Add (eSlot);
				}
			}
		}

		while (oldEffects.Count > 0) {

			EffectSlot eSlot = oldEffects [0];
			eSlot.m_effect = null;
			oldEffects.RemoveAt (0);
			m_effectPool.Remove (eSlot);
		}
	}
}
