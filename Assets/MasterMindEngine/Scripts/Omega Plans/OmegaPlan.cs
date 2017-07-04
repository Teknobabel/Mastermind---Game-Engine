using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class OmegaPlan : ScriptableObject {

	[System.Serializable]
	public struct Phase {

		public List<OPGoal> m_goals;

		public List<Asset> m_mandatoryAssets;
	}

	public string m_name;

	public Phase[] m_phases;
}
