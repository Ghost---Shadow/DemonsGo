using UnityEngine;
using System.Collections;

public class Torso : MonoBehaviour {
	public GameObject[] torsos;	
	public float[] damages;

	private GameObject currentTorso;
	private int index;

	void Start(){
		currentTorso = null;
	}

	public void spawnRandomTorso(){
		if(currentTorso != null){
			DestroyImmediate(currentTorso);
		}
		index = (int)Random.Range(0,torsos.Length);
		currentTorso = (GameObject) Instantiate(torsos[index],transform.position,Quaternion.identity);
		currentTorso.transform.parent = this.transform;
	}

	public void attack(PlayerController player){
		SwipeDetector.SwipeDirection direction = (SwipeDetector.SwipeDirection)((int)Random.Range(0,4));
		Debug.Log("Attacking from "+direction);
		player.damage(damages[index]);
	}
}
