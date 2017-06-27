using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Trait : ScriptableObject {

	public enum Type {
		None,
		Skill,
		Gift,
		Flaw,
		Dynamic,
		Status,
		Resource,
	}

	public string m_name;

	public Type m_type = Type.None;
}
