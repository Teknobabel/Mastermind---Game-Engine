using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurnPhase_EndTurn : TurnPhase {

	public override IEnumerator StartTurnPhase ()
	{
		Debug.Log ("Starting End Turn Phase");
		yield return null;
	}

	public override IEnumerator DoTurnPhase ()
	{
		Debug.Log ("Do End Turn Phase");

		// evaluate any event triggers

		// check if intel should be spawned

		yield return null;
	}

	public override IEnumerator EndTurnPhase ()
	{
		Debug.Log ("Ending End Turn Phase");
		yield return null;
	}
}
