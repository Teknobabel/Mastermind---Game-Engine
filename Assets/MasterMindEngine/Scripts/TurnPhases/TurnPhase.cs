using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPhase : ScriptableObject {

	public bool m_proceedToNextPhase = false;

//	public virtual IEnumerator StartTurnPhase ()
//	{
//		Debug.Log ("Starting Turn Phase");
//		yield return null;
//	}

	public virtual void DoTurnPhase ()
	{
		Debug.Log ("Do Turn Phase");
	}

//	public virtual IEnumerator EndTurnPhase ()
//	{
//		Debug.Log ("Ending Turn Phase");
//		yield return null;
//	}
}
