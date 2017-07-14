using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_IncrementTurnNumber : Action {

	public override void ExecuteAction ()
	{
		GameEngine.instance.game.IncrementTurn ();
	}
}
