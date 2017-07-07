using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_EndPhase : Action {

	public override void ExecuteAction ()
	{
		GameEngine.instance.ProgressTurn ();
	}
}
