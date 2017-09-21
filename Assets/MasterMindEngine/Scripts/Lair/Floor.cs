using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Floor : ScriptableObject, IEffectable {

	public string m_name = "Null";

	public int m_startingHenchmenSlots = 1;

	public List<Mission> m_missions = new List<Mission> ();

	private List<Mission> m_completedUpgrades = new List<Mission> ();

	private int m_level = 1;

	private EffectPool m_effectPool = new EffectPool();

	public virtual void Initialize (){
	}

	public virtual void Destroy (){
	}

	public List<Mission> completedUpgrades {get{return m_completedUpgrades;} set{ m_completedUpgrades = value; }}
	public int level {get{ return m_level; }set{ m_level = value; }}
	public EffectPool effectPool {get{return m_effectPool;}}
}
