using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurnPhase_PlayerPhase : TurnPhase {

	public override IEnumerator StartTurnPhase ()
	{
		Debug.Log ("Starting Player Phase");

		// refill command pool

		// pay henchmen

		// get turn summary

		yield return null;
	}

	public override IEnumerator DoTurnPhase ()
	{
		Debug.Log ("Do Player Phase");

		// get player input

		yield return null;
	}

	public override IEnumerator EndTurnPhase ()
	{
		Debug.Log ("Ending Player Phase");

		// refill empty henchmen hire slots

		// update infamy

		yield return null;
	}
}
