using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OxygenTimer : MonoBehaviour
{
    public float timeLeft = 180f; //gameplay time
    public bool timerActive = true;

    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel;

    public MonoBehaviour playerControllerScript;

    void Start()
    {
        //Unpause game and hide Game Over screen at the start
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (timerActive)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                DisplayTime(timeLeft);
            }
            else
            {
                timeLeft = 0;
                timerActive = false;
                DisplayTime(timeLeft);
                GameOver();
            }
        }
    }

    // Converts seconds to minutes and seconds 
    void DisplayTime(float currentTime)
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        if (seconds < 10)
        {
            timerText.text = "Time Remaining : " + minutes + ":0" + seconds;
        }
        else
        {
            timerText.text = "Time Remaining : " + minutes + ":" + seconds;
        }
    }

    // Open game over UI, unlocks the mouse and turns off player movement
    void GameOver()
    {
        gameOverPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Freez camera
        if (playerControllerScript != null)
        {
            playerControllerScript.enabled = false;
        }

        Time.timeScale = 0f; 
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Load Main Menu 
    public void GoToMainMenu(int sceneIndex)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneIndex);
    }
}