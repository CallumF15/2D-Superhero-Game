using UnityEngine;
using System.Collections;
using Leap;

public class cursor : MonoBehaviour {

	Controller m_leapController;
	HandList hList;
	Camera camera;
	int count = 1;

	// Use this for initialization
	void Start () {
		m_leapController = new Controller ();
		m_leapController.EnableGesture (Gesture.GestureType.TYPESCREENTAP);
		camera = GameObject.FindGameObjectWithTag ("MainCamera").gameObject.camera;
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
		Vector3 cursorPosition = camera.WorldToScreenPoint (handPosition);





		//Debug.Log ("hand pos: " + handPosition);
		bb (cursorPosition);

//		for (int i = 0; i < count; i++) {
//			//bb (cursorPosition);
		//}
		//create a temp 2D sphere to act as cursor
	}

	void bb(Vector3 pos){
		//pos.z = 0;
		Instantiate (GameObject.CreatePrimitive (PrimitiveType.Cube), pos, Quaternion.identity);
	}
}
