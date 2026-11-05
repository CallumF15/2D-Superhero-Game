using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour 
{
	private Camera camera;

    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnQuit;


    // Use this for initialization
    void Start () {
        camera = Camera.main;

        btnPlay.onClick.AddListener(PlayGame);
        btnSettings.onClick.AddListener(Settings);
        btnQuit.onClick.AddListener(Quit);
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
