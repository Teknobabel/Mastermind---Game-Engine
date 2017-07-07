using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurnPhase_PlayerPhase : TurnPhase {

//	public override IEnumerator StartTurnPhase ()
//	{
//		Debug.Log ("Starting Player Phase");
//
//		// refill command pool
//
//		// pay henchmen
//
//		// get turn summary
//
//		yield return null;
//	}

	public override void DoTurnPhase ()
	{
		Debug.Log ("Starting Player Phase");

		// get player input

//		GameEngine.instance.NextTurnPhase ();
		Debug.Log ("Ending Player Phase");

	}

//	public override IEnumerator EndTurnPhase ()
//	{
//		Debug.Log ("Ending Player Phase");
//
//		// refill empty henchmen hire slots
//
//		// update infamy
//
//		yield return null;
//	}
}
