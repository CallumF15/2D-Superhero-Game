using UnityEngine;
using System.Collections;

public class SpeedBoost{

	public float Speed;
	public float SpeedMultipliers;
	public float Durations; //may change to time?
	
	public SpeedBoost(float speed, float speedmultiplier, float duration)
	{
		Speed = speed;
		SpeedMultipliers = speedmultiplier;
		Durations = duration;
	}
	
	void SpecialEffects(){

		GameObject obj =  GameObject.FindGameObjectWithTag ("Player");
		obj.transform.GetComponent<Rigidbody>().linearVelocity = new Vector3 (Speed, 0, 0);
	}
}
