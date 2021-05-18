using UnityEngine;
using System.Collections;

public class setup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//player doesn't go above camera height
		//camera only moves horizontally
	}


	void b(){

		Camera cam = GameObject.FindGameObjectWithTag ("MainCamera").gameObject.GetComponent<Camera>();
		Vector3 playerPos = GameObject.FindGameObjectWithTag ("Player").transform.position;
		Vector3 bossPos = transform.position;

		Rect camRect = cam.rect;

		float width = cam.rect.width;
		float height = cam.rect.height;

		Vector3 newPlayerPos = cam.ScreenToWorldPoint (playerPos);
		Vector3 newBossPos =  cam.ScreenToWorldPoint (bossPos);
	}
}
