using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		healthbar.HealthBarImage = GameObject.Find("redHealthbar").GetComponent<Image>();
		healthbar.SetHealthBarValue(1);
	}
	
	// Update is called once per frame
	void Update () {
	
		
	}

	/// <summary>
	/// If player is hit by enemy's attack. Reduce health and destroy attack game object
	/// </summary>
	/// <param name="other"></param>
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "attack") { 
		
			healthbar.SetHealthBarValue(healthbar.GetHealthBarValue() - 0.1f);
			Destroy(other.gameObject);
		}
	}
}
