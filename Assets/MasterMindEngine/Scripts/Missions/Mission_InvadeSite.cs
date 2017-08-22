using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_InvadeSite : Mission {

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		float revealChance = 0.5f;
		float stealChance = 0.5f;
		float removeTraitChance = 0.25f;

		if (plan.m_result == MissionPlan.Result.Success) {

			Site site = plan.m_missionSite;

			// foreach asset

			foreach (Site.AssetSlot aSlot in site.assets) {

				// if hidden, chance to reveal

				if (aSlot.m_state == Site.AssetSlot.State.Hidden && Random.Range(0.0f, 1.0f) <= revealChance) {

					Action_RevealAsset revealAsset = new Action_RevealAsset ();
					revealAsset.m_playerID = 0;
					revealAsset.m_assetSlot = aSlot;
					GameController.instance.ProcessAction (revealAsset);
				}

				// if revealed, chance to steal

				if (aSlot.m_state == Site.AssetSlot.State.Revealed && Random.Range(0.0f, 1.0f) <= stealChance) {

					//					if (aSlot.m_site != null) {
					//
					//						aSlot.m_site.RemoveAsset (aSlot);
					//					}

					Player player = GameEngine.instance.game.playerList [0];
					Action_GainAsset gainAsset = new Action_GainAsset ();
					gainAsset.m_asset = aSlot.m_asset;
					gainAsset.m_player = player;
					GameController.instance.ProcessAction (gainAsset);
				}

			}

			// foreach site trait

			List<SiteTrait> traits = new List<SiteTrait> ();

			foreach (SiteTrait t in site.traits) {

				traits.Add (t);

			}

			foreach (SiteTrait t in traits)
			{
				// chance to remove trait

				if (Random.Range (0.0f, 1.0f) <= removeTraitChance) {

					Action_RemoveSiteTrait removeTrait = new Action_RemoveSiteTrait ();
					removeTrait.m_siteID = site.id;
					removeTrait.m_trait = t;
					GameController.instance.ProcessAction (removeTrait);
				}
			}

			// raise alert level of all sites in region

			Region region = GameEngine.instance.game.regionList [site.regionID];

			foreach (Site s in region.sites) {

				if (s.id != site.id) {
					
					Action_ChangeAlertLevel increaseAlertLevel = new Action_ChangeAlertLevel ();
					increaseAlertLevel.m_playerID = 0;
					increaseAlertLevel.m_amount = 2;
					increaseAlertLevel.m_siteID = s.id;
					GameController.instance.ProcessAction (increaseAlertLevel);
				}
			}

		}
	}
}
