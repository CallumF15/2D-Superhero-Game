using UnityEngine;
using System.Collections;

public class scaleAttack : MonoBehaviour
{
	GameObject[] attackObj;
	GameObject attackObject;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
//		attackObj = GameObject.FindGameObjectsWithTag ("attack");
//		for (int i = 0; i < attackObj.Length; i++) {
//			float value = Mathf.Lerp (attackObj[i].transform.localScale.x, 5.0f, Time.deltaTime / 2);
//			attackObj[i].transform.localScale = new Vector3 (value, value, 1);
//		}

		float value = Mathf.Lerp (transform.localScale.x, 0.5f, Time.deltaTime / 2);
		transform.localScale = new Vector3 (value, value, 1);
	}
}

