using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameEvent {

	Player_HiringPoolChanged,
	Player_HenchmenPoolChanged,
	Player_CommandPoolChanged,
	Turn_PlayerPhaseStarted,
	Player_LairChanged,
	Player_NewMissionStarted,
	Player_MissionCompleted,
	Player_OmegaPlanChanged,
	Player_AssetsChanged,
	Player_OmegaPlanGoalCompleted,
	Henchmen_NewStateChanged,
	Player_MissionCancelled,
	Henchmen_RemovedFromHireable,
	Player_NotificationReceived,
}
