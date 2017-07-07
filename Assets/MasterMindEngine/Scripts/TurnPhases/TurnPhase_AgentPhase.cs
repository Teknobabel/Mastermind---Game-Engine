using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurnPhase_AgentPhase : TurnPhase {

//	public override IEnumerator StartTurnPhase ()
//	{
//		Debug.Log ("Starting Agent Phase");
//		yield return null;
//	}

	public override void DoTurnPhase ()
	{
		Debug.Log ("Starting Agent Phase");

		// do AI for agents not on a mission

		// resolve missions

		// spawn new agents if needed

//		GameEngine.instance.ProgressTurn ();
		Debug.Log ("Ending Agent Phase");
	}

//	public override IEnumerator EndTurnPhase ()
//	{
//		Debug.Log ("Ending Agent Phase");
//		yield return null;
//	}
}
