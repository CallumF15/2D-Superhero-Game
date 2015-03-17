using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {

	public float offsetX = 15;
	public float offsetY = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Camera cam = GameObject.FindGameObjectWithTag ("MainCamera").gameObject.camera;
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		var playerPos = player.transform.position;

		float playerPositionX = playerPos.x;
		float playerPositionY = playerPos.y;

		Vector3 cameraPos = new Vector3 (playerPositionX + offsetX, playerPositionY, -5);
		cam.transform.position = cameraPos;
	}
}
