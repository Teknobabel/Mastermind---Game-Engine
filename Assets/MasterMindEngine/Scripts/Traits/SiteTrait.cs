using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SiteTrait : Trait {

	public Trait m_requiredTrait;

	public virtual void TraitAdded (Site site)
	{

	}

	public virtual void TraitRemoved (Site site)
	{

	}

	public override string GetDescription ()
	{
		string s = "Any Missions run at this location will require the following Traits:";
		s += "\n" + m_requiredTrait.m_name;

		return s;

	}
}
