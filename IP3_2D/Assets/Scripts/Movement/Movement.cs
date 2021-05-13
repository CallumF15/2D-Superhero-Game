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
	public float flySpeed = 17.0f;
	public float jumpPower = 1.0f;
	public float fallPower = 1.0f;
	public float rotationSpeed = 1.5f;

	private float pitchAngle;

	[HideInInspector]
	public float getPitchAngle { get; set; }

	[HideInInspector]
	private float speed = .3f;
	
	/// <summary>
	/// Resets the rotation back to 0 
	/// Doesn't just set back to 0, does it gradually over time (needs fixed as it appears it just sets it to 0)
	/// </summary>
	void resetRotation() 
	{
		float objZrot = transform.eulerAngles.z;
		float tParam = .3f;

		if (objZrot != 0) 
		if (objZrot >= 0 && objZrot <= 100) {
			tParam = Time.deltaTime * speed;
			float value = Mathf.Lerp(transform.rotation.z, 0, tParam);
			transform.parent.rotation = Quaternion.Euler(new Vector3(0,0, value));
		}
		else if (objZrot >= 270 && objZrot <= 360) {
			tParam = Time.deltaTime * speed;
			float value = Mathf.Lerp(transform.rotation.z, 0, tParam);
			transform.parent.rotation = Quaternion.Euler(new Vector3(0,0, value));
		}
	}

	/// <summary>
	/// Runs the appropriate code depending on the type of movement
	/// </summary>
	public void CheckMovementType (MovementType type)
	{
		Debug.Log ("Type: " + type);

		switch (type) {

		case MovementType.stand:
			transform.GetComponent<Rigidbody>().velocity = new Vector3 (0, 0, 0);
			resetRotation ();
			break;
		case MovementType.fly: 
				//float rot = getPitchAngle * Time.deltaTime * rotationSpeed;
				//gameObject.transform.Rotate(0,0, rot);
				transform.position += transform.right * Time.deltaTime * flySpeed;
			break;
		case MovementType.fire:
				playerAttack(); 
			break;

		default:
			// unknown type! 
			// there should probably be some error-handling
			// here, maybe an exception
			break;
		}
	}

	private void playerAttack()
    {
		GameObject firePrefab = Resources.Load("bossAttacks/fireAttack") as GameObject;
		firePrefab.transform.localScale = new Vector3(0.1f, 0.1f, 1);
		Vector3 playerPos = transform.position;
		Instantiate(firePrefab, new Vector3(playerPos.x + 5, playerPos.y, playerPos.z), Quaternion.identity); //spawn object
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
