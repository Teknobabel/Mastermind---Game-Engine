using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPhase : ScriptableObject {

	public virtual IEnumerator StartTurnPhase ()
	{
		Debug.Log ("Starting Turn Phase");
		yield return null;
	}

	public virtual IEnumerator DoTurnPhase ()
	{
		Debug.Log ("Do Turn Phase");
		yield return null;
	}

	public virtual IEnumerator EndTurnPhase ()
	{
		Debug.Log ("Ending Turn Phase");
		yield return null;
	}
}
