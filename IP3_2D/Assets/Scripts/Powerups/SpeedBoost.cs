using UnityEngine;
using System.Collections;

public class SpeedBoost{

	public float speed;
	public float speedMultipliers;
	public float durations; //may change to time?
	
	public SpeedBoost(float speed, float speedmultiplier, float duration)
	{
		speed = speed;
		speedmultiplier = speedmultiplier;
		durations = durations;
	}
	
	void SpecialEffects(){

		GameObject obj =  GameObject.FindGameObjectWithTag ("Player");
		obj.transform.rigidbody.velocity = new Vector3 (speed, 0, 0);
	}
}
