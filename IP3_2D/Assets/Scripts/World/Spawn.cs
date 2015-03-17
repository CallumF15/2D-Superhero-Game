using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public float speed = 1.0f;

	public int cubeCount = 5;
	
	// Use this for initialization
	void Start () {
		int count = 0;

		for (int i = 0; i < cubeCount; i++) {
			count = count + 2;

			Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(count,0,0), Quaternion.identity);
		}
		
		GameObject[] go = UnityEngine.GameObject.FindGameObjectsWithTag ("cubes");


//		float x = 0;
//			float y = 0;
//		float z = 0;
//		int count = 0;
//
//		foreach (GameObject g in go) {
//
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(){

		GameObject[] go = UnityEngine.GameObject.FindGameObjectsWithTag ("cube");

		foreach (GameObject g in go) {
			print("COLLISION");
					Destroy (g);
					}

	}
}
