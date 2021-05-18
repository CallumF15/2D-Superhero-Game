using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class powerMessage : MonoBehaviour
{
	public Text message;
	public string powerupMessage = "POWERUP ACTIVATED"; //default message text
	public float disappearTime = 2.0f; //time till message is cleared

	// Use this for initialization
	void Start ()
	{
		message.text = "";
	}
	
	IEnumerator delayTimer (float delayTimer)
	{
		yield return new WaitForSeconds (delayTimer);
		ClearMessage (); 
	}

	void OnCollisionEnter(Collision hit){
		if (hit.gameObject.tag == "powerups") {
			message.text = powerupMessage;
			Destroy(hit.gameObject);
			StartCoroutine (delayTimer (disappearTime));
		}
	}

	/// <summary>
	/// Clear the body of text
	/// </summary>
	void ClearMessage(){
		message.text = "";
	}
}

