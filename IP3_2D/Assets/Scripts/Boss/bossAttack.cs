using UnityEngine;
using System.Collections;

public class bossAttack : MonoBehaviour
{
	public enum attackTypes
	{
		wait,
		singleFire,
		multipleShots,
		spreadShot,
		superAttack
	}
	;

	public Rigidbody2D attackPrefab;
	Rigidbody2D attackInstance;
	attackTypes bossAttackTypes;
	Vector3 playerPos;
	float timeElapsed;
	float lastStateCheck = 0f;

	/// below variables deal with boss attack delays
	public float startGameDelay = 10.0f;  //start of game delay
	public float shotDelay = 5.0f;       //delay between each shots
	public float shotSpeed = 5.0f; 		  //speed of shots

	//below variables deal with the number of projectiles for each attack type
	public int singleShotCount = 1;
	public int spreadShotCount = 15; //must be dividle by 3
	public int superAttackShotCount = 30;

	//below variables deal with the projectiles speed
	public int singleShotSpeed = 1;
	public int spreadShotSpeed = 15; //must be dividle by 3
	public int superAttackShotSpeed = 30;

	//below variables deal with delay between projectiles
	public int singleShotDelay = 1;
	public int spreadShotDelay = 15; //must be dividle by 3
	public int superAttackShotDelay = 30;

	float timeFrequency;
	bool canFire;
	private int shotCount = 0;
	private int currentCount = 0;

	// Use this for initialization
	void Start ()
	{
		bossAttackTypes = attackTypes.singleFire;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log ("attack type" + bossAttackTypes);
		timeElapsed = Time.time;
		//StartCoroutine (delayTimer (startGameDelay)); 
	}

	/// <summary>
	/// Game has to wait so many seconds before boss will shoot (could change to till boss appears)
	/// </summary>
	IEnumerator delayTimer (float delayTimer)
	{
		yield return new WaitForSeconds (delayTimer);
		spawnObject (); //replace with CheckAttackTypes();
	}

	void determineAttack ()
	{
		int health = bossHealth.getHealth;
		float freq;

		for (int i = health; i > 0;) {
			freq = i * 0.01f;
			//lower health, higher frequency
		}
	}
	
	void spawnObject ()
	{
		CheckAttackTypes ();

		if (currentCount <= shotCount)
		if (lastStateCheck + shotDelay < timeElapsed) {
			
			canFire = true;
			Vector3 bossPos = transform.position;
			Vector3 fireSpawn = new Vector3 (bossPos.x - 1, bossPos.y, 0.0f);
			
			attackInstance = Instantiate (attackPrefab, fireSpawn, Quaternion.identity) as Rigidbody2D;
			attackInstance.gravityScale = 0.0f;
			attackInstance.velocity = (new Vector2 (-shotSpeed, 0));
			
			currentCount++;
			lastStateCheck = timeElapsed;
		} else
			canFire = false;

		if (currentCount >= shotCount)
			currentCount = 0;
	}

	void CheckAttackTypes ()
	{
		if (canFire = true) {
			switch (bossAttackTypes) {

			case attackTypes.singleFire:
				shotCount = singleShotCount;
				shotSpeed = singleShotSpeed;
				shotDelay = singleShotDelay;
				spawnObject();
				break;

			case attackTypes.multipleShots:
				shotCount = 5;

				break;
			case attackTypes.spreadShot:
			//shotCount must be dividle by 3! 
			//shotCount will fire projectiles straight, diagonally up and down 

				float spreadDelay = 2.0f;
				shotDelay = spreadDelay;

				shotCount = 15;
				float shotSpread = shotCount / 3;
				float spreadShotSpeed = 3.0f;
				shotSpeed = spreadShotSpeed;

				if (shotCount < shotCount / 3) {
					//transform.parent.Rotate(0,0, rot)
					attackInstance.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
				}

				if (shotCount < (shotSpread * 2) && shotCount > shotSpread) {
					attackInstance.transform.rotation = Quaternion.Euler (new Vector3 (45, 0, 0));
				}

				if (shotCount > (shotSpread * 2) && shotCount <= 15) {
					attackInstance.transform.rotation = Quaternion.Euler (new Vector3 (-45, 0, 0));
					//attackInstance not instantiated yet (spawnObject needs to be called first)
				}

				spawnObject ();

				break;
			case attackTypes.superAttack:
				shotCount = 20;

				break;
			default:

				break;

			}
		}
	}
}
