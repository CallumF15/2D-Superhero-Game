using UnityEngine;
using System.Collections;

public enum attackTypes
{
	wait,
	singleFire,
	multipleShots,
	spreadShot,
	superAttack
};

public class bossAttack : MonoBehaviour
{

	public Rigidbody2D attackPrefab;
	Rigidbody2D attackInstance;

	attackTypes bossAttackTypes;

	Vector3 playerPos;
	public float startGameDelay = 10.0f;
	public float shotDelay = 10.0f;
	public float shotSpeed = 5.0f;
	float timeElapsed;
	float lastStateCheck = 0f;
	public float defaultDelay = 2.0f;

	//deals with how often boss fires type of shot
	float timeFrequency;
	bool canFire;
	public int shotCount = 5;
	private int currentCount = 0;

	// Use this for initialization
	void Start ()
	{
		bossAttackTypes = attackTypes.wait;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeElapsed = Time.time;

		//anticipate player movement
		//Should A.I know about map boundary? if so, fires below/above player
		//when their near top/bottom of boundary

		StartCoroutine (delayTimer (startGameDelay));
	}

	IEnumerator delayTimer (float delayTimer)
	{
		yield return new WaitForSeconds (delayTimer);
		spawnObject (); //replace with CheckAttackTypes();
	}

	void spawnObject ()
	{
		if(currentCount <= shotCount)
			if (lastStateCheck + defaultDelay < timeElapsed) {

			canFire = true;
			Vector3 bossPos = transform.position;
		
			Vector3 fireSpawn;
			fireSpawn.x = bossPos.x - 1;
			fireSpawn.y = bossPos.y;
			fireSpawn.z = 0.0f;

			attackInstance = Instantiate (attackPrefab, fireSpawn, Quaternion.identity) as Rigidbody2D;
			attackInstance.gravityScale = 0.0f;
			attackInstance.velocity = (new Vector2 (-shotSpeed, 0));

			currentCount++;
			lastStateCheck = timeElapsed;
		} else
			canFire = false;
	}
	
	void spawnObject2 ()
	{
		bossAttackTypes = attackTypes.spreadShot;
		CheckAttackTypes ();

		if(currentCount <= shotCount)
		if (lastStateCheck + defaultDelay < timeElapsed) {
			
			canFire = true;
			Vector3 bossPos = transform.position;
			
			Vector3 fireSpawn;
			fireSpawn.x = bossPos.x - 1;
			fireSpawn.y = bossPos.y;
			fireSpawn.z = 0.0f;
			
			attackInstance = Instantiate (attackPrefab, fireSpawn, Quaternion.identity) as Rigidbody2D;
			attackInstance.gravityScale = 0.0f;
			attackInstance.velocity = (new Vector2 (-shotSpeed, 0));
			
			currentCount++;
			lastStateCheck = timeElapsed;
		} else
			canFire = false;
	}

	void CheckAttackTypes ()
	{
		//other attack ideas:
		//projectiles stop dead in their tracks, and starting floating down (spaces inbetween each projectile)
		//heatseeker projectile (follows the player, not too precisely so its dodgable)
		//portal barriage (portal opens up releasing trees/rocks/monsters?)
		//

		switch (bossAttackTypes) {

		case global::attackTypes.singleFire:
			//allocate shot counts?
			shotCount = 1;
			shotDelay = 1.0f;
			attackInstance.velocity = new Vector2 (-shotSpeed, 0);

			break;
		case global::attackTypes.multipleShots:
			shotCount = 5;

			break;
		case global::attackTypes.spreadShot:
			//shotCount must be dividle by 3! 
			//shotCount will fire projectiles straight, diagonally up and down 

			float spreadDelay = 2.0f;
			defaultDelay = spreadDelay;

			shotCount = 15;
			float shotSpread = shotCount / 3;
			float spreadShotSpeed = 3.0f;
			shotSpeed = spreadShotSpeed;

			if(shotCount < shotCount / 3){
				//transform.parent.Rotate(0,0, rot)
				attackInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
			}

			if(shotCount < (shotSpread * 2) && shotCount > shotSpread){
				attackInstance.transform.rotation = Quaternion.Euler(new Vector3(45, 0, 0));
			}

			if(shotCount > (shotSpread * 2) && shotCount <= 15){
				attackInstance.transform.rotation = Quaternion.Euler(new Vector3(-45, 0, 0));
				//attackInstance not instantiated yet (spawnObject needs to be called first)
			}

			spawnObject2();

			break;
		case global::attackTypes.superAttack:
			shotCount = 20;

			break;
		default:

			break;

		}
	}

	//if player hasn't been hit once, boss fires multiple shots, more frequent superAttacks
	//if player's health is below 25% the boss's superAttacks are less frequent


}
