using UnityEngine;

public class IntroBriefing : MonoBehaviour
{
    public GameObject briefingPanel;
    public FPController playerController;

    void Start()
    {
        Time.timeScale = 0f;
        PauseMenuManager.GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        if (briefingPanel != null)
        {
            briefingPanel.SetActive(true);
        }
    }

    public void CloseBriefing()
    {
        if (briefingPanel != null)
        {
            briefingPanel.SetActive(false);
        }

        Time.timeScale = 1f;
        PauseMenuManager.GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }
}