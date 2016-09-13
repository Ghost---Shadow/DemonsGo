using UnityEngine;
using System.Collections;

public class Head : MonoBehaviour {
	public GameObject[] heads;	

private GameObject currentHead;

	void Start(){
		currentHead = null;
	}

	public void spawnRandomHead(){
		if(currentHead != null){
			DestroyImmediate(currentHead);
		}
		int index = (int)Random.Range(0,heads.Length);
		currentHead = (GameObject) Instantiate(heads[index],transform.position,Quaternion.identity);
	}
}