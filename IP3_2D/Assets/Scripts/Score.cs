using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	public GUIText message;


	private int score = 0;


	// Use this for initialization
	void Start () {
		message.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void calculateScore(){

		//if player picks up coin score + 1;

		message.text = "Score: " + score;
	}
}
