using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
	public Transform targetPlayer;
	Camera playerCam;
	public float cameraDistance = 5.0f; // Camera Distance for Third Person View.
	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;

	//For Mouse Look
	public Vector2 mouseLook;
	//For Mouse Rotate Axis
	public Vector2 rotateVert;
	//For X Rotate of Camera
	public Quaternion camRotateX;
	//For XY Rotate of Camera
	public Quaternion camRotateXY;

	public Vector3 lookOffset; //For Third Person Look.


	// Use this for initialization
	void Start()
	{
		playerCam = GetComponent<Camera>();
		lookOffset = playerCam.transform.position - targetPlayer.transform.position;
	}

	// Update is called once per frame
	void Update()
	{

		CameraControl();

	}

	public void CameraControl()
	{
		mouseLook += rotateVert;
		//Setting Angle for the Y Rotation
		mouseLook.y = Mathf.Clamp(mouseLook.y, -40, 40);
		//Setting Angle for the X Rotation
		//mouseLook.x = Mathf.Clamp (mouseLook.x, -90, 90);

		//Rotate on Both X and Y Use this Camera Method
		camRotateXY = Quaternion.Euler(-mouseLook.y, mouseLook.x, 0);

		//Rotate on Only X Use this Camera Method
		camRotateX = Quaternion.Euler(0, mouseLook.x, 0);

        //Rotate Player With Camera on Move and Rotate Camera Around the Player in Idle State
        //if (getActions.checkActions == true)
        //{
        //	targetPlayer.eulerAngles = new Vector3(0, camRotateX.eulerAngles.y, 0);
        //}
        //else
        //{
        //	Vector3 lookPoint = targetPlayer.transform.position;
        //	playerCam.transform.LookAt(lookPoint + lookOffset);
        //}

        Vector3 lookPoint = targetPlayer.transform.position;
        playerCam.transform.LookAt(lookPoint + lookOffset);

        //First Person Look with Only X Rotate and Both X and Y Rotate Just Change the Rotation Axies of Cameras
        //Vector3 position=targetPlayer.position-(camRotateX*(new Vector3(0,0,0)+new Vector3(0,-lookOffset.y,-lookOffset.z)));

        //Third Person Look With Only X Rotate
        //Vector3 position=targetPlayer.position-(camRotateX * Vector3.forward * cameraDistance + new Vector3(0,-lookOffset.y,0));

        //Third Person Look With Both X and Y Rotate
        Vector3 position = targetPlayer.position - (camRotateXY * Vector3.forward * cameraDistance + new Vector3(0, -lookOffset.y, 0));

		//First Person Look With Both X and Y Rotate
		//Vector3 position=targetPlayer.position-(camRotateX *(new Vector3(0,0,0)+ new Vector3(0,-lookOffset.y,-lookOffset.z)));

		//Turn it on For Rotation Only on X Axis
		//playerCam.transform.rotation = camRotateX;

		//Turin it on for Both Axis Rotation
		playerCam.transform.rotation = camRotateXY;
		playerCam.transform.position = position;
	}
}
