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
		message.text = "Score: " + score;
	}

	void OnCollisionEnter(Collision hit){
		Debug.Log ("COLLISION COIN");

		if (hit.gameObject.tag == "coin") {
			calculateScore();
			Destroy (hit.gameObject);
		}
	}

	void calculateScore(){
		score = score + 1;
	}
}
