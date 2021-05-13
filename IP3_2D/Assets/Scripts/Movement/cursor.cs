using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cursor : MonoBehaviour {

	GameObject cursorObject;
	Camera camera;
	
	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); //check this
		cursorObject = GameObject.FindGameObjectWithTag ("cursor");
	}
	
	// Update is called once per frame
	void Update () {
        hoveringButton(cursorObject.transform.position);
	}


	void hoveringButton(Vector3 pos)
	{
        Debug.DrawLine(pos, new Vector3(pos.x, pos.y, 5), Color.red);

        RaycastHit hit;
        if (Physics.Raycast(pos, Vector3.forward, out hit, 100.0F))
        {

            switch (hit.transform.name)
            {
                case "btnPlay":

                    Debug.Log("btnPlay");
                    //loadingBar(pos);
                    //Application.LoadLevel("game");
                    break;

                case "btnStore":
                    //loadingBar(pos);
                    Debug.Log("btnStore");
                    break;

                case "btnSettings":
                    //loadingBar(pos);
                    Debug.Log("btnSettings");
                    break;

                case "btnQuit":
                    //loadingBar(pos);
                    Debug.Log("btnQuit");
                    break;
            }
        }
    }

	void loadingBar(Vector3 pos){

		int width = UnityEngine.Screen.width;
		GetComponent<Renderer>().material.SetFloat ("_Cutoff", Mathf.InverseLerp (0, width, pos.x));
	}
}
