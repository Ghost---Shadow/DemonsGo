using UnityEngine;
using System.Collections;

public class Torso : MonoBehaviour {
	public GameObject[] torsos;	

	private GameObject currentTorso;

	void Start(){
		currentTorso = null;
	}

	public void spawnRandomTorso(){
		if(currentTorso != null){
			DestroyImmediate(currentTorso);
		}
		int index = (int)Random.Range(0,torsos.Length);
		currentTorso = (GameObject) Instantiate(torsos[index],transform.position,Quaternion.identity);
	}
}
