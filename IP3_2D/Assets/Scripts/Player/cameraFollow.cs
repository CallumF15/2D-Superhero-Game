using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {

	public float offsetX = 15;
	public float offsetY = 0;
	public float offsetZ = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		//print("Player Postiion Camera " + player.transform.position);
		var playerPos = player.transform.position;

		float playerPositionX = playerPos.x;
		float playerPositionY = playerPos.y;

		Vector3 cameraPos = new Vector3 (playerPositionX + offsetX, playerPositionY, -10);
		transform.position = cameraPos;
	}
}
