using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Placeholder
	public static float getDamageTakenBy(int type,int spell){
		return type == spell ? .1f : .2f;
	}
}
