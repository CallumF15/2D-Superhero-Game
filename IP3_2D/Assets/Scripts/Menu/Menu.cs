using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour 
{
	private Camera camera;

    [SerializeField] public Button btnPlay;
    [SerializeField] public Button btnSettings;
    [SerializeField] public Button btnQuit;


    // Use this for initialization
    void Start () {
        camera = Camera.main;

        btnPlay.onClick.AddListener(PlayGame);
        btnSettings.onClick.AddListener(Settings);
        btnQuit.onClick.AddListener(Quit);
    }
	
	// Update is called once per frame
	void Update () {
        //hoveringButton(cursorObject.transform.position);
	}

    void PlayGame()
    {
        Debug.Log("btnPlay");
        SceneManager.LoadScene("BossChase");
    }

    void Settings()
    {
        Debug.Log("btnSettings");
        //SceneManager.LoadScene("Settings");
    }

    void Quit()
    {  
        Debug.Log("btnQuit");
        Application.Quit();
    }
}
