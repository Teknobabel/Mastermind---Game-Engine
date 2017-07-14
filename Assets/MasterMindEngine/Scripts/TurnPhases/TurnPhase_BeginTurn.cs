using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurnPhase_BeginTurn : TurnPhase {

//	public override IEnumerator StartTurnPhase ()
//	{
//		Debug.Log ("Starting Start Turn Phase");
//		yield return null;
//	}

	public override void DoTurnPhase ()
	{
		Debug.Log ("Starting Start Turn Phase");

		// increase turn number 

		Action_IncrementTurnNumber incTurn = new Action_IncrementTurnNumber ();
		GameController.instance.ProcessAction (incTurn);

//		GameEngine.instance.NextTurnPhase ();
		Debug.Log ("Ending Start Turn Phase");
	}

//	public override IEnumerator EndTurnPhase ()
//	{
//		Debug.Log ("Ending Start Turn Phase");
//		yield return null;
//	}

}
