using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Floor : ScriptableObject {

	public string m_name = "Null";

	public int m_startingHenchmenSlots = 1;

	public List<Mission> m_missions = new List<Mission> ();

	private List<Mission> m_completedUpgrades = new List<Mission> ();

	public virtual void Initialize (){
	}

	public virtual void Destroy (){
	}

	public List<Mission> completedUpgrades {get{return m_completedUpgrades;} set{ m_completedUpgrades = value; }}

}
