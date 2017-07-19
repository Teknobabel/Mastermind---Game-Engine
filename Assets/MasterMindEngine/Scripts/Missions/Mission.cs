﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Mission : ScriptableObject {

	public string m_name = "Null";

	public Trait[] m_requiredTraits;

	public int 
	m_cost = 1,
	m_duration = 1;
}