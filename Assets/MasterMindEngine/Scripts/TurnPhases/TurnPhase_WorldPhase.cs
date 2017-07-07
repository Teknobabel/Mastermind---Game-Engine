using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurnPhase_WorldPhase : TurnPhase {

//	public override IEnumerator StartTurnPhase ()
//	{
//		Debug.Log ("Starting World Phase");
//
//
//
//		yield return null;
//	}

	public override void DoTurnPhase ()
	{
		Debug.Log ("Starting World Phase");

		// henchmen & agents in limbo

		// policies

		// declare war

//		GameEngine.instance.NextTurnPhase ();
		Debug.Log ("Ending World Phase");

	}

//	public override IEnumerator EndTurnPhase ()
//	{
//		Debug.Log ("Ending World Phase");
//		yield return null;
//	}
}
