using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerHeight : MonoBehaviour {

	public GameObject player;
	public Text message;

	public float playerHeightLimit = 100.0f; //maximum height limit
	public float timeRemaining = 11.0f; //time left to return or player dies
	private float timeLeft;

	float currentPosX;
	float prevPosX;

	// Use this for initialization 
	void Start () {
		timeLeft = timeRemaining;
		prevPosX = -16;
		currentPosX = 1;
	}
	
	// Update is called once per frame
	void Update () {
		limitHeight ();
	}

	/// <summary>
	/// 
	/// </summary>
	private void limitHeight(){
		currentPosX = player.transform.position.x;

		float playerHeight = player.transform.position.y;

		if (playerHeight >= playerHeightLimit) { //player has hit map height boundary
			Vector3 playerPos = player.transform.position;
			playerPos.y = playerHeightLimit;
			player.transform.position = playerPos;
		}

		if (prevPosX > currentPosX) //going in wrong direction 
		{
			prevPosX = currentPosX;
			WrongDirectionMessage();
		}

		if (playerHeight <= 0) { //player has hit the ground
			Vector3 playerPos = player.transform.position;
			playerPos.y = 0;
			player.transform.position = playerPos;
		}

		if (prevPosX < currentPosX) {
			timeRemaining = timeLeft;
			message.text = "";
		}

		prevPosX = currentPosX;
	}

	/// <summary>
	/// Displays message with time remaining 
	/// </summary>
	private void WrongDirectionMessage()
	{
		timeRemaining -= Time.deltaTime;
		//Debug.Log("timeR: " + timeRemaining);
		int timeLeft = (int)timeRemaining;

		message.text = "Wrong direction! " + timeLeft.ToString() + " seconds to turn round"; ;

		if (timeRemaining < 0) {
			message.text = "GAME OVER!";
			Destroy(player);
		}
	}
}
