using UnityEngine;
using System.Collections;
using Leap;

public class LeapGesture : MonoBehaviour
{

	public float degreeX { get; set; }
	public float degreeY { get; set; }
	public float degreeZ { get; set; }
	public float pitchDegrees { get; set; }
	public float yawDegrees { get; set; }
	public float rollDegrees { get; set; }
	public Frame _frame { get; set; }

	public Controller m_leapController { get; set; }

	private Movement movement;
	private float fixedAngle = 90.0f;

	// Use this for initialization
	void Start ()
	{
		movement = gameObject.AddComponent<Movement> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void CalculateHandAngle ()
	{
		var prevFrame = m_leapController.Frame (10); 
		
		float rotationX = _frame.RotationAngle (prevFrame, Vector.XAxis);
		float rotationY = _frame.RotationAngle (prevFrame, Vector.YAxis);
		float rotationZ = _frame.RotationAngle (prevFrame, Vector.ZAxis);

		degreeX = rotationX * Mathf.Rad2Deg;
		degreeY = rotationY * Mathf.Rad2Deg;
		degreeZ = rotationZ * Mathf.Rad2Deg;
	
//		Debug.Log ("X DEGREE: " + degreeX);
//		Debug.Log ("Y DEGREE: " + degreeY);
//		Debug.Log ("Z DEGREE: " + degreeZ);
	}

	void HandRotation (Hand hand)
	{
		//The normal vector to the palm.
		//If your hand is flat, this vector will point downward, or “out” of the front surface of your palm.

		float pitch = hand.Direction.Pitch;
		float yaw = hand.Direction.Yaw;
		float roll = hand.PalmNormal.Roll;

		pitchDegrees = pitch * Mathf.Rad2Deg;
		yawDegrees = yaw * Mathf.Rad2Deg;
		rollDegrees = roll * Mathf.Rad2Deg;;

	 //   Debug.Log ("Pitch: " + pitchDegrees);
//		Debug.Log ("Yaw: " + yawDegrees);
		//Debug.Log ("Roll: " + rollDegrees);
	}

	public void HandPlayerMovement (Frame frame, Hand hand, Controller controller)
	{
		_frame = frame;
		MovementType type;

		HandRotation (hand);
		CalculateHandAngle ();

		//Hand rotation for left/right movement below:


		//Commented below is if hand is in fist.
//		float strength = hand.GrabStrength;
//		
//		if(strength == 1)
//			Debug.Log("FIST");

//		if (hand == null) {
//			Debug.Log("NOT null");
//			type = MovementType.stand;
//			movement.CheckMovementType (type, movement.getfaceRight);
//		}

		if (yawDegrees >= -20 && yawDegrees <= 20) {
		
			if (rollDegrees >= -100 && rollDegrees <= 100) {
				type = MovementType.stand;
				movement.CheckMovementType (type, movement.getfaceRight);
			}

			//note: if we have up/down movement with pitch it'll collide with left/right gestures



			//note: since pitch goes to negative it makes our speed negative :/
			//note: since our Flying code sets pitch to 45, this code will not work correctly unless we resolve this someway

//			if(pitchDegrees <= 45 && pitchDegrees <= -45){
//				type = MovementType.flyAscend;
//				movement.CheckMovementType (type, movement.getfaceRight);
//			}
		}



	
		//Flying code: 

		//note: In order to start flying, hand's pitch must be around 10 & -10 degrees? (yet to be decided)

		if (hand.IsLeft) { //Weird movement compared to Left movement

			if (yawDegrees >= 45 && yawDegrees <= 180) {
				type = MovementType.fly;

				if (pitchDegrees <= fixedAngle && pitchDegrees >= -fixedAngle) {
					movement.getPitchAngle = pitchDegrees;
					movement.CheckMovementType (type, true);
				}

				//CheckAngle(type, true);
			}
		}
//		if (hand.IsRight) {
//			if (yawDegrees <= -45 && yawDegrees >= -180) {
//				type = MovementType.fly;
//
//				if(pitchDegrees <= fixedAngle && pitchDegrees >= -fixedAngle){
//				movement.getPitchAngle = pitchDegrees;
//				movement.CheckMovementType (type, false);
//				}
//
//				//CheckAngle(type, false);
//
//			}
//		}
	}

	/// <summary>
	/// Makes sure angle doesn't exceed 45/-45 on pitch axis
	/// </summary>
	void CheckAngle(MovementType type, bool direction)
	{
		if(pitchDegrees > fixedAngle)
		{
			Debug.Log ("greater");
			movement.getPitchAngle = fixedAngle;
			//movement.CheckMovementType (type, direction);
		}else if(pitchDegrees < -fixedAngle)
		{			
			print ("lower");
			movement.getPitchAngle = -fixedAngle;
			//movement.CheckMovementType (type, direction);
		}
	}
}
