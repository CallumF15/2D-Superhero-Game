using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

	public float rotationSpeed = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//transform.rotation = Quaternion.Euler (new Vector3 (0, rotationSpeed * Time.deltaTime, 0));
		//transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
		transform.Rotate (0,0,rotationSpeed*Time.deltaTime);
	}
}
