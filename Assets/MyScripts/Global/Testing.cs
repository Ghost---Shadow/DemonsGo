using UnityEngine;
using System.Collections;

public class Testing : MonoBehaviour {

	void OnEnable()
    {
        SwipeDetector.Swipe += myLog;
    }
    
    
    void OnDisable()
    {
        SwipeDetector.Swipe -= myLog;
    }

	public void myLog(SwipeDetector.SwipeDirection direction){
		Debug.Log(direction);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
