using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button btnResume;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnQuit;

    [SerializeField] private GameObject pauseMenuUI;

    void Start()
    {
        btnResume.onClick.AddListener(ResumeGame);
        btnSettings.onClick.AddListener(Settings);
        btnQuit.onClick.AddListener(Quit);
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        if (pauseMenuUI.activeSelf)
            ResumeGame();
        else
            PauseGame();
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        GameManager.Instance.PauseGame();
    }

    void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        GameManager.Instance.ResumeGame();
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
