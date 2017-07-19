using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Floor : ScriptableObject {

	public string m_name = "Null";

	public int m_startingHenchmenSlots = 1;

	public List<Mission> m_missions = new List<Mission> ();



}
