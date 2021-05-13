using UnityEngine;
using System.Collections;

public class planeScale : MonoBehaviour {

	Camera cam;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera>();
		float height = cam.orthographicSize;
		float width = height * Screen.width / Screen.height;
		transform.localScale = new Vector3(width, 0.1f, height);

//		float height = 2.0f * Mathf.Tan (0.5f * cam.fieldOfView * Mathf.Deg2Rad) * cam.transform.position.z - transform.position.z;
//		float width = height * Screen.width / Screen.height;
//		transform.localScale = new Vector3(width, 0.1f, height);



	}
	
	// Update is called once per frame
	void Update () {
	}
}
