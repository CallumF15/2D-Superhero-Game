using UnityEngine;
using System.Collections;

public class healthbar : MonoBehaviour {

	int health = 10;

	int width, height;

	Texture healthTexture;
	GameObject healthObject;



	// Use this for initialization
	void Start () {
		width = Screen.width / 4;
		height = Screen.height / 4;

		healthTexture =  Resources.Load("healthBar") as Texture2D;
	}
	
	// Update is called once per frame
	void Update () {


		//if player gets hit reduce healthbar by 1


	
	}


}
