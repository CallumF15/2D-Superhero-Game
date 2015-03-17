using UnityEngine;
using System.Collections;

public class move : MonoBehaviour
{

	public float Speed = 3.0f;
	public float delayTime = 1.0f;
	public float offsetX = 30;
	Vector3 bossPos, playerPos;

		
	// Update is called once per frame
	void Update ()
	{
		moveBoss();
	}

	
	void moveBoss ()
	{
		GameObject obj = GameObject.FindGameObjectWithTag ("Player");
		
		playerPos = obj.transform.position;
		bossPos = transform.position;
		
		//boss position on Y axis has to be somewhat near the players slight delay
		float val = trackPlayer ();

		bossPos.x = playerPos.x + offsetX;
		bossPos.y = val;
		transform.position = bossPos;
	}

	float trackPlayer()
	{
		float tParam = Time.deltaTime * Speed;
		float lerpVal = Mathf.Lerp (bossPos.y, playerPos.y, tParam);
		return lerpVal;
	}
}
