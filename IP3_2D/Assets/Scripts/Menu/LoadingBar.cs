using UnityEngine;
using System.Collections;

public class LoadingBar : MonoBehaviour {

	public Vector3 handPosition { get; set; }

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
		renderer.material.SetFloat ("_Cutoff", Mathf.InverseLerp (0, Screen.width, Input.mousePosition.x));
	}
}
