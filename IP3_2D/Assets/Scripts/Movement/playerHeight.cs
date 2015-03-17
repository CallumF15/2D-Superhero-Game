using UnityEngine;
using System.Collections;

public class playerHeight : MonoBehaviour {

	GameObject player;
	public GUIText message;

	public float playerHeightLimit = 25.0f;
	public float timeRemaining = 11.0f;
	private float timeLeft;
	
	float currentPosX, prevPosX;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		timeLeft = timeRemaining;
		currentPosX = 1;
		prevPosX = 0;
	}
	
	// Update is called once per frame
	void Update () {
		limitHeight ();
	}

	void limitHeight(){
		Debug.Log ("currentPosX " + currentPosX);

		currentPosX = player.transform.position.x;
		float playerHeight = player.transform.position.y;

		if (playerHeight >= playerHeightLimit) {
			Vector3 playerPos = player.transform.position;
			playerPos.y = playerHeightLimit;
			player.transform.position = playerPos;
		}

		if (prevPosX > currentPosX) {
			prevPosX = currentPosX;
			WrongDirectionMessage ();
		}

		if (prevPosX < currentPosX)
			timeRemaining = timeLeft;

		Debug.Log ("prevPosX " + prevPosX);
		prevPosX = currentPosX;
	}

	void WrongDirectionMessage()
	{
		timeRemaining -= Time.deltaTime;
		int timeLeft = (int)timeRemaining;

		Debug.Log ("message hitttt");
		message.text = "Wrong direction! " + timeLeft.ToString() + " seconds to turn round"; ;

		if (timeRemaining < 0) {
			message.text = "GAME OVER!";
			Destroy(player);
		}
	}
}
