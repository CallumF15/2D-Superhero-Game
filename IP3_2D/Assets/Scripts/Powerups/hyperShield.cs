using UnityEngine;
using System.Collections;

public class hyperShield : IPowerups {

	public float damage { get; set; }
	public float damageMultiplier { get; set; }
	public float duration { get; set; }

	public hyperShield(float damages, float damagemultiplier, float durations)
	{
		damage = damages;
		damageMultiplier = damagemultiplier;
		duration = durations;
	}

	IEnumerator delayTimer (float delayTimer)
	{
		yield return new WaitForSeconds (delayTimer);
		ActivatePowerup ();
	}

	void ActivatePowerup(){
	}
	
	public void SpecialEffect(){
		
		GameObject obj =  GameObject.FindGameObjectWithTag ("Player");
		int health = playerHealth.getHealth;

		bool isActive = false;

		if (isActive == true) {
	
			//if player takes damage, damage negated

		}
	}

	void OnCollisionEnter(Collision hit){


		if (hit.gameObject.tag == "powerups")
		if (hit.gameObject.name == "Shield") {
			delayTimer(duration);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

                                  
	}
}
