using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Leap;

public class cursor : MonoBehaviour {

	Controller m_leapController;
	GameObject cursorObject;
	HandList hList;
	Camera camera;
	int count = 1;
	
	// Use this for initialization
	void Start () {
		m_leapController = new Controller ();
		m_leapController.EnableGesture (Gesture.GestureType.TYPESCREENTAP);
		camera = GameObject.FindGameObjectWithTag ("MainCamera").gameObject.camera;
		cursorObject = GameObject.FindGameObjectWithTag ("cursor");
	}
	
	// Update is called once per frame
	void Update () {
		setupLeap();

	}


	void setupLeap()
	{
		Frame frame = m_leapController.Frame ();
		hList = frame.Hands;

		if (m_leapController.IsConnected) {
	
			if (hList[0] != null && frame.Hands.Count == 1 && hList[0].IsValid) { //if hand is not over LeapMotion == null & only 1 hand
				//call method
				handToScreenPosition();
			}
		}
	}

	void handToScreenPosition(){

		var palmPos = hList [0].PalmPosition;
		//Vector3 handPosition = hList [0].PalmPosition.ToUnityScaled ();
		Vector3 handPosition = hList[0].PalmPosition.ToUnity();
		//Vector3 cursorPosition = camera.WorldToScreenPoint (handPosition);
		//cursorPosition.z = 0;
		handPosition.z = -5;
		handPosition.y = handPosition.y - 150;

		if(handPosition.y >= 45)
			handPosition.y = 45;
		if(handPosition.y <= -45)
			handPosition.y = -45;

		if (handPosition.x >= 75)
			handPosition.x = 75;
		if (handPosition.x <= -75)
			handPosition.x = -75;


		cursorObject.transform.position = handPosition;

		hoveringButton (handPosition);
	}

	void hoveringButton(Vector3 pos)
	{
		Debug.DrawLine(pos, Vector3.forward, Color.red);

		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.forward, out hit, 100.0F)) {

			switch(hit.transform.name)
			{
			case "btnPlay":
				Debug.Log("btnPlay");
				Application.LoadLevel("game");
				break;

			case "btnStore":

				break;

			case "btnSettings":

				break;

			case "btnExit":

				break;
			}
		}
	}
}
