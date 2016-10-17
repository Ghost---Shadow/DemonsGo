using UnityEngine;
using System.Collections.Generic;
using OpenCVForUnity;

public class DeviceCameraController : MonoBehaviour
{
	public int lineThickness = 5;	// Thickness of vectors
	public int maxCorners = 10; // Number of features
	public double minDistance = 20; // Min distance to count as movement
	public double qualityLevel = .01; // Quality level
	public float sensitivity = .01f; // TODO: remove
	public float screenSize = 10.0f;

	private bool enableDrawingPoints = true;

    private WebCamTexture mCamera; // Frame recorded from webcam
    private Texture2D texture; // Unity3D texture of the webcam frame

	private Mat rgbaMat; // Webcam frame in OpenCV format 
    private Mat matOpFlowThis; // Optical flow for this frame
    private Mat matOpFlowPrev; // Optical flow for last frame
    private MatOfPoint MOPcorners; // Corner features detected
    private MatOfPoint2f mMOP2fptsThis; // Feature set of this frame
    private MatOfPoint2f mMOP2fptsPrev; // Feature set of last frame
    private MatOfPoint2f mMOP2fptsSafe; // Safe copy of feature set
    private MatOfByte mMOBStatus; // Status returned by OpenCV after calculating optical flow
    private MatOfFloat mMOFerr; // Stores error, if any
	private Color32[] colors; // 32bit color buffer for interconversion
	private Scalar colorRed = new Scalar(255, 0, 0, 125); // Red color

    void Start()
    {
        for (int i = 0; i < WebCamTexture.devices.Length; i++)
            Debug.Log(WebCamTexture.devices[i].name);
		
        mCamera = new WebCamTexture();
        matOpFlowThis = new Mat();
        matOpFlowPrev = new Mat();
        MOPcorners = new MatOfPoint();
        mMOP2fptsThis = new MatOfPoint2f();
        mMOP2fptsPrev = new MatOfPoint2f();
        mMOP2fptsSafe = new MatOfPoint2f();
        mMOBStatus = new MatOfByte();
        mMOFerr = new MatOfFloat();

		mCamera.Play();

		rgbaMat = new Mat(mCamera.height, mCamera.width, CvType.CV_8UC4);
        texture = new Texture2D(mCamera.width, mCamera.height, TextureFormat.RGBA32, false);
		colors = new Color32[mCamera.width * mCamera.height];

        GetComponent<Renderer>().material.mainTexture = texture;
        
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();		
		
		if(Input.GetMouseButtonUp(0))
			enableDrawingPoints = !enableDrawingPoints;
		
		if(mCamera.width < 100)
			return; // Camera not ready
		
		float aspect = (float)mCamera.width/(float)mCamera.height;
		transform.localScale = new Vector3(aspect*screenSize,1,screenSize);
		
		Utils.webCamTextureToMat(mCamera, rgbaMat, colors);
		
        updateFeatures();
		Video.calcOpticalFlowPyrLK(matOpFlowPrev, matOpFlowThis, mMOP2fptsPrev, mMOP2fptsThis, mMOBStatus, mMOFerr);
		if(!mMOBStatus.empty()){
			if(enableDrawingPoints)
				drawPoints();
			updateCameraFrustum();
		}
		
		Utils.matToTexture2D(rgbaMat, texture, colors);
		gameObject.GetComponent<Renderer>().material.mainTexture = texture;
		
		//gameObject.GetComponent<Renderer>().material.mainTexture = mCamera;
    }

	private void updateFeatures(){
		if (mMOP2fptsPrev.rows() == 0){
            Imgproc.cvtColor(rgbaMat, matOpFlowThis, Imgproc.COLOR_RGBA2GRAY);
            matOpFlowThis.copyTo(matOpFlowPrev);
            Imgproc.goodFeaturesToTrack(matOpFlowPrev, MOPcorners, maxCorners, qualityLevel, minDistance);	
            mMOP2fptsPrev.fromArray(MOPcorners.toArray());
            mMOP2fptsPrev.copyTo(mMOP2fptsSafe);
        } else {
            matOpFlowThis.copyTo(matOpFlowPrev);
            Imgproc.cvtColor(rgbaMat, matOpFlowThis, Imgproc.COLOR_RGBA2GRAY);
            Imgproc.goodFeaturesToTrack(matOpFlowThis, MOPcorners, maxCorners, qualityLevel, minDistance);
            mMOP2fptsThis.fromArray(MOPcorners.toArray());
            mMOP2fptsSafe.copyTo(mMOP2fptsPrev);
            mMOP2fptsThis.copyTo(mMOP2fptsSafe);
        }
	}

	private void drawPoints(){
		List<Point> cornersPrev = mMOP2fptsPrev.toList();
		List<Point> cornersThis = mMOP2fptsThis.toList();
		List<byte> byteStatus = mMOBStatus.toList();

		int x = 0;
		int y = byteStatus.Count - 1;

		for (x = 0; x < y; x++)	{
			if (byteStatus[x] == 1)	{
				Point pt = cornersThis[x];
				Point pt2 = cornersPrev[x];
				Core.circle(rgbaMat, pt, 5, colorRed, lineThickness - 1);
				Core.line(rgbaMat, pt, pt2, colorRed, lineThickness);
			}
		}
	}

	private void updateCameraFrustum(){
		List<Point> cornersPrev = mMOP2fptsPrev.toList();
		List<Point> cornersThis = mMOP2fptsThis.toList();
		List<byte> byteStatus = mMOBStatus.toList();

		int x = 0;
		int y = byteStatus.Count - 1;

		Vector2 netMovement = Vector2.zero;

		for (x = 0; x < y; x++)	{
			if (byteStatus[x] == 1)	{
				Vector2 delta = new Vector2((float)(cornersThis[x].x - cornersPrev[x].x),
											(float)(cornersThis[x].y - cornersPrev[x].y));
				netMovement += delta;
			}
		}
		float scale = netMovement.magnitude/y;
		netMovement = scale * netMovement.normalized * sensitivity;
		transform.parent.position += new Vector3(-netMovement.x,netMovement.y,0.0f);
		//transform.parent.Rotate(Vector3.up,netMovement.x);
	}
}
