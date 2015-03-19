using UnityEngine;
using System.Collections;

public class altitude : MonoBehaviour {

	public GUIText message;

	// Use this for initialization
	void Start () {
		message.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		float height = GameObject.Find ("Player").transform.position.y;
		int newHeight = (int)height;
		message.text = "Altitude: " + newHeight;
	}
}
