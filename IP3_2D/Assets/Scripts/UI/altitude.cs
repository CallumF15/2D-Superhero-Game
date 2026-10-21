using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Altitude : MonoBehaviour {

	[SerializeField] private Transform player;

	public Text message;

	// Use this for initialization
	void Start () {
		message.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		float height = player.transform.position.y;
		int newHeight = Mathf.RoundToInt(height); //cast to an integer value for display purposes
		message.text = "Altitude: " + newHeight;
	}
}
