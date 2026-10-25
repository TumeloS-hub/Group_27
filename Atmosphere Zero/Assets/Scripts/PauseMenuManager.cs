using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; 
public class PauseMenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject pauseMenuCanvas; 

    public static bool GameIsPaused = false;

    void Update()
    {
        // this toggle pause menu when pressing Escape 
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f; // Resumes game time
        GameIsPaused = false;

        // Re-lock mouse cursor for first person look
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f; // Freezes game time
        GameIsPaused = true;

        // Unlocks mouse cursor so buttons are clickable
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; 
        GameIsPaused = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("MainMenu"); 
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Player Has Quit The Game");
    }
}