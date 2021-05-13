using UnityEngine;
using System.Collections;

public class hyperShield : MonoBehaviour {

	public float duration = 5.0f;
	GameObject player;
	
	IEnumerator delayTimer (float delayTimer)
	{
		yield return new WaitForSeconds (delayTimer);
		deactivate ();
	}

	public void SpecialEffect(){

		var boxCollider = gameObject.AddComponent<BoxCollider> ();
		boxCollider.name = "HyperShield";
		boxCollider.center = new Vector3 (8.5f, 0, 0);
		boxCollider.size = new Vector3 (2, 10, .2f);
	}

	void deactivate(){

		var blah = gameObject.GetComponent <BoxCollider> ();

		if (blah.name == "HyperShield") {
			Destroy(blah);
		}
	}

	void OnCollisionEnter(Collision hit){

		//Debug.Log ("HYPER SHIELD");
	
		if (hit.gameObject.name == "shield") {
			//Debug.Log("special effect");
			SpecialEffect();
			delayTimer(duration);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

                                  
	}
}
