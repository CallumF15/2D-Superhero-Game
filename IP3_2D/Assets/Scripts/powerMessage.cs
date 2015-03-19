using UnityEngine;
using System.Collections;

public class powerMessage : MonoBehaviour
{
	public GUIText message;
	public string powerupMessage = "POWERUP ACTIVATED";
	public float disappearTime = 2.0f;

	public float timeElapsed;
	public float lastStateCheck;

	// Use this for initialization
	void Start ()
	{
		message.text = "";
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	IEnumerator delayTimer (float delayTimer)
	{
		yield return new WaitForSeconds (delayTimer);
		clearMessage (); //replace with CheckAttackTypes();
	}

	void OnCollisionEnter(Collision hit){
		Debug.Log ("COLLISION TEST");
		
		if (hit.gameObject.tag == "powerups") {
			message.text = powerupMessage;
			Destroy(hit.gameObject);
			StartCoroutine (delayTimer (disappearTime));
		}
	}

	void clearMessage(){
		message.text = "";
		lastStateCheck = timeElapsed;
	}
}

