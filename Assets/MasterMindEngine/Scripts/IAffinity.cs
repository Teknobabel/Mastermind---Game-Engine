using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAffinity {

	Dictionary<int, AffinitySlot> affinityList { get;}

	int GetAffinityScore (int targetID, IAffinity target);
	void UpdateAffinity (int targetID, int amount, IAffinity target);
}
