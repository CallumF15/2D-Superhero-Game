using UnityEngine;
using System.Collections;
using Leap;

public enum MovementType
{
	stand,
	fall,
	fly,
	flyAscend,
	flyDescend,
	fire
};

public class Movement : MonoBehaviour
{
	public float flySpeed = 15.0f;
	public float jumpPower = 1.0f;
	public float fallPower = 1.0f;
	public float rotationSpeed = 1.0f;

	[HideInInspector]
	public bool getfaceRight { get; set; }

	[HideInInspector]
	public float getPitchAngle { get; set; }

	[HideInInspector]
	private float speed = 0.3f;
	
	/// <summary>
	/// Resets the rotation back to 0 
	/// Doesn't just set back to 0, does it gradually over time (needs fixed as it appears it just sets it to 0)
	/// </summary>
	void resetRotation() 
	{
		float objZrot = transform.eulerAngles.z;
		float tParam = 0.5f;

		if (objZrot != 0) 
		if (objZrot >= 0 && objZrot <= 100) {
			tParam = Time.deltaTime * speed;
			float value = Mathf.Lerp(transform.rotation.z, 0, tParam);
			transform.parent.rotation = Quaternion.Euler(new Vector3(0,0, value));
		}
		else if (objZrot >= 300 && objZrot <= 360) {
			tParam = Time.deltaTime * speed;
			float value = Mathf.Lerp(transform.rotation.z, 0, tParam);
			transform.parent.rotation = Quaternion.Euler(new Vector3(0,0, value));
		}
	}

	/// <summary>
	/// Runs the appropriate code depending on the type of movement
	/// </summary>
	public void CheckMovementType (MovementType type, bool faceRight)
	{
		//Change 2D face direction to right/left
		getfaceRight = faceRight;

		Debug.Log ("Type: " + type);
		
		if (faceRight) { 
			Debug.Log ("FACERIGHT");
			transform.localScale = new Vector3 (1f, 1f, 1f); 
		} else {
			Debug.Log ("FACELEFT");
			transform.localScale = new Vector3 (-1f, 1f, 1f);
		}
		
		//Movement related below (will make more complex later):

		switch (type) {

		case MovementType.stand:
			transform.parent.rigidbody.velocity = new Vector3 (0, 0, 0);
			resetRotation ();
			break;

		//Flying code below:
		
		case MovementType.fly: 
			float rot = getPitchAngle * Time.deltaTime * rotationSpeed;

			if (faceRight) {
				transform.parent.Rotate(0,0, rot);
				transform.parent.position += transform.right * Time.deltaTime * flySpeed;
			}
			break;

		case MovementType.flyAscend:
			Debug.Log("angle: " + getPitchAngle);
			float ascendSpeed = getPitchAngle / 10;
			Debug.Log("acs speed: " + ascendSpeed);
			transform.parent.rigidbody.velocity = new Vector3 (0, ascendSpeed, 0);
			break;

		case MovementType.flyDescend:
			float descentSpeed = getPitchAngle / 10;		
			transform.parent.rigidbody.velocity = new Vector3 (0, -descentSpeed, 0);
			break;
		case MovementType.fire:
			//fire code goes here

			break;

		default:
			// unknown type! 
			// there should probably be some error-handling
			// here, maybe an exception
			break;
		}
	}

	/// <summary>
	/// if the player changes say from -45 to 20 within a split second, the game will slowly increment -45 to 20 over time so 
	/// that the player's angle isn't automatically changed to 20.
	/// </summary>
	public void gradualChange()
	{
		float currentAngle;
		float prevAngle = 0.0f;

		currentAngle = getPitchAngle;
		float sum = currentAngle - prevAngle;

		if (sum >= 1) {
		}
	}


}
