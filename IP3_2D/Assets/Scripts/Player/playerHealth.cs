using UnityEngine;
using System.Collections;

public class playerHealth : MonoBehaviour {

	public static int getHealth { get; set; }

	public float minHealth = 0;
	public float maxHealth = 10;

	public float damage;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
		Mathf.Clamp (getHealth, minHealth, maxHealth);
	}

	void reduceHealth()
	{
		getHealth--;
	}
}
