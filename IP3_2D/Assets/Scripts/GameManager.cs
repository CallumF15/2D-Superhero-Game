using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the overall game state, including pausing, resuming,
/// and handling player death.
///
/// Controls the game's time scale and manages gameplay scripts
/// to ensure gameplay properly stops when paused or when the
/// player dies.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool IsGameOver { get; private set; }
    public bool IsPaused { get; private set; }

    private readonly List<MonoBehaviour> pausedScripts = new();

    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    public GameState CurrentState { get; private set; } = GameState.Playing;

    private void Awake()
    {
        // Prevent multiple GameManager instances.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    /// <summary>
    /// Handles player death and stops gameplay.
    /// </summary>
    public void PlayerDied()
    {
        if (CurrentState == GameState.GameOver)
            return;

        CurrentState = GameState.GameOver;

        PauseScripts();
        Time.timeScale = 0f;

        Debug.Log("Game Over!");
    }

    /// <summary>
    /// Pauses the game and disables active scripts.
    /// </summary>
    public void PauseGame()
    {
        if (CurrentState != GameState.Playing)
            return;

        CurrentState = GameState.Paused;

        PauseScripts();
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Resumes gameplay and restores previously active scripts.
    /// </summary>
    public void ResumeGame()
    {
        if (CurrentState != GameState.Paused)
            return;

        CurrentState = GameState.Playing;

        Time.timeScale = 1f;
        ResumeScripts();
    }

    /// <summary>
    /// Restarts the current scene.
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    /// <summary>
    /// Disables active scripts while preserving their enabled states.
    /// </summary>
    private void PauseScripts()
    {
        pausedScripts.Clear();

        MonoBehaviour[] scripts =
            FindObjectsByType<MonoBehaviour>(
                FindObjectsInactive.Include
            );

        foreach (MonoBehaviour script in scripts)
        {
            if (script == this || !script.enabled)
                continue;

            pausedScripts.Add(script);
            script.enabled = false;
        }
    }

    /// <summary>
    /// Restores scripts that were enabled before pausing.
    /// </summary>
    private void ResumeScripts()
    {
        foreach (MonoBehaviour script in pausedScripts)
        {
            if (script != null)
                script.enabled = true;
        }

        pausedScripts.Clear();
    }
}
