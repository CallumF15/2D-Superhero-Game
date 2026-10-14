using UnityEngine;
using System.Collections;

public class PlayerCameraController : MonoBehaviour {

	[SerializeField] private float offsetX = 15;
    [SerializeField] private float offsetY = 0;

    [SerializeField] private GameObject player;

	// Update is called once per frame
	void Update () 
	{
		var playerPosition = player.transform.position;

		float playerPositionX = playerPosition.x;
		float playerPositionY = playerPosition.y;

		Vector3 cameraPosition = new Vector3 (playerPositionX + offsetX, playerPositionY + offsetY, -10f);
		transform.position = cameraPosition;
	}
}
