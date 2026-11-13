using UnityEngine;
using UnityEngine.SceneManagement;

public class RescueTrigger : MonoBehaviour
{
    public GameObject victoryPanel;
    public OxygenTimer oxygenTimer;
    public MonoBehaviour playerControllerScript;

    private bool hasRescued = false;

    void Start()
    {
        victoryPanel.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // Only trigger if the Player touches the zone 
        if (other.CompareTag("Player") && !hasRescued)
        {
            hasRescued = true;
            WinGame();
        }
    }

    // Stop timer, call victory UI, and unlock mouse
    void WinGame()
    {
        if (oxygenTimer != null)
        {
            oxygenTimer.StopTimer();
        }

        victoryPanel.SetActive(true);

        // Unlock mouse for button click
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerControllerScript != null)
        {
            playerControllerScript.enabled = false;
        }

        Time.timeScale = 0f; // Freeze game
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu(int sceneIndex)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneIndex);
    }
}