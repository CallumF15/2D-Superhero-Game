using UnityEngine;
using System.Collections;
using Leap;

/// <summary>
/// 
/// </summary>
public class testFly : MonoBehaviour
{
	private LeapGesture gesture;
	Controller m_leapController;
	
	// Use this for initialization
	void Start ()
	{
		m_leapController = new Controller ();
		gesture = gameObject.AddComponent<LeapGesture> ();

		if (transform.parent == null) {
			Debug.LogError ("LeapFly must have a parent object to control");
		}
	}

	void assignHands (Frame frame, Hand leftHand, Hand rightHand)
	{
		//if left hand overlaps right hand left hand acts as right
		if (leftHand.PalmPosition.x > rightHand.PalmPosition.x) {
			leftHand = rightHand;
			rightHand = frame.Hands [0];
		}

		if (rightHand.PalmPosition.x < leftHand.PalmNormal.x) {
			rightHand = leftHand;
			leftHand = frame.Hands[0]; //check this value [0]
		}
	}

	
	void FixedUpdate ()
	{
		Frame frame = m_leapController.Frame ();

		Hand hand = frame.Hands.Leftmost;
		HandList hList = frame.Hands;
		
		//NOTE: IF using right hand for flying Angles will be WEIRD
		//NOTE: Flip your right hand upside down confuses it for LEFT!

		if (m_leapController.IsConnected) {

			Debug.Log ("CONNECTED");

			gesture._frame = frame;
			gesture.m_leapController = m_leapController;

			Debug.Log("valid hand" + hand.IsValid);

			if (hand != null && frame.Hands.Count <= 1 && hand.IsValid) { //if hand is not over LeapMotion == null & only 1 hand
			
				//	Time.timeScale = 1.0f;
				gesture.HandPlayerMovement (hand);
			}
		} else {
			//Time.timeScale = 0.0f;
			//not connected -- pause game -- await connection
		}

	
		//if we have 1 hand active


			// if closed fist, then stop the plane and slowly go backwards.
//			if (frame.Fingers.Count < 3) {
//				transform.parent.rigidbody.velocity = new Vector3 (speed, 0, 0);
//			}
	
	}

}