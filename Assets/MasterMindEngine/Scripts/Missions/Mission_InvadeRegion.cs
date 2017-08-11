using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_InvadeRegion : Mission {

	public Region m_targetRegion;

	public override void CompleteMission (MissionPlan plan)
	{
		base.CompleteMission (plan);

		float revealChance = 0.3f;
		float stealChance = 0.2f;
		float removeTraitChance = 0.1f;

		int numRandomSites = 6;

		if (plan.m_result == MissionPlan.Result.Success) {

			List<Site> randSelectionList = new List<Site> ();

			foreach (KeyValuePair<int, Region> pair in GameEngine.instance.game.regionList) {

				if (pair.Value.m_regionName == m_targetRegion.m_regionName) {

					foreach (Site s in pair.Value.sites) {

						s.UpdateAlert (2);

						foreach (Site.AssetSlot aSlot in s.assets) {

							// if hidden, chance to reveal

							if (aSlot.m_state == Site.AssetSlot.State.Hidden && Random.Range (0.0f, 1.0f) <= revealChance) {

								Action_RevealAsset revealAsset = new Action_RevealAsset ();
								revealAsset.m_playerID = 0;
								revealAsset.m_assetSlot = aSlot;
								GameController.instance.ProcessAction (revealAsset);
							}

							// if revealed, chance to steal

							if (aSlot.m_state == Site.AssetSlot.State.Revealed && Random.Range (0.0f, 1.0f) <= stealChance) {

								Player player = GameEngine.instance.game.playerList [0];
								Action_GainAsset gainAsset = new Action_GainAsset ();
								gainAsset.m_asset = aSlot.m_asset;
								gainAsset.m_player = player;
								GameController.instance.ProcessAction (gainAsset);
							}

						}

						// foreach site trait

						List<SiteTrait> traits = new List<SiteTrait> ();

						foreach (SiteTrait t in s.traits) {

							traits.Add (t);

						}

						foreach (SiteTrait t in traits) {
							// chance to remove trait

							if (Random.Range (0.0f, 1.0f) <= removeTraitChance) {

								Action_RemoveSiteTrait removeTrait = new Action_RemoveSiteTrait ();
								removeTrait.m_siteID = s.id;
								removeTrait.m_trait = t;
								GameController.instance.ProcessAction (removeTrait);
							}
						}

					}
//					break;
				} else {

					foreach (Site s in pair.Value.sites) {

						randSelectionList.Add (s);
					}
				}
			}


			for (int i = 0; i < numRandomSites; i++) {

				if (randSelectionList.Count > 0) {

					int r = Random.Range (0, randSelectionList.Count);

					Site thisSite = randSelectionList [r];
					randSelectionList.RemoveAt (r);

					thisSite.UpdateAlert (2);
				}
			}
		}
	}
}
