using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurnPhase_BeginTurn : TurnPhase {

	public override IEnumerator StartTurnPhase ()
	{
		Debug.Log ("Starting Start Turn Phase");
		yield return null;
	}

	public override IEnumerator DoTurnPhase ()
	{
		Debug.Log ("Do Start Turn Phase");

		// increase turn number 

		yield return null;
	}

	public override IEnumerator EndTurnPhase ()
	{
		Debug.Log ("Ending Start Turn Phase");
		yield return null;
	}

}
