using UnityEngine;
using System.Collections;

public class Webcam : MonoBehaviour {

	public float screenSize = 100.0f;
	
	private WebCamTexture webcamTexture;

     void Start () 
     {
         webcamTexture = new WebCamTexture();
		 webcamTexture.Play();
		gameObject.GetComponent<Renderer>().material.mainTexture = webcamTexture;        
     }
	 
	 void Update(){
		 if(webcamTexture.width < 100)
			return; // Camera not ready
		
		float aspect = (float)webcamTexture.width/(float)webcamTexture.height;
		transform.localScale = new Vector3(aspect*screenSize,screenSize,1);
	 }
}
