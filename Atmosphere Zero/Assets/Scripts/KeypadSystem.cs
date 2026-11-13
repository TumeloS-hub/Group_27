using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class KeypadSystem : MonoBehaviour
{
    public string correctCode = "4028";
    private string currentInput = "";

    public TextMeshProUGUI displayText;
    public GameObject keypadPanel;
    public GameObject doorToUnlock;
    public MonoBehaviour playerControllerScript;

    void Start()
    {
        if (keypadPanel != null)
        {
            keypadPanel.SetActive(false);
        }

        UpdateDisplay();
    }

    void Update()
    {
        // Allow the player to press Escape to back out of the keypad
        if (keypadPanel.activeSelf && Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseKeypad();
        }
    }

    public void OpenKeypad()
    {
        keypadPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerControllerScript != null)
        {
            playerControllerScript.enabled = false;
        }

        Time.timeScale = 0f;
    }

    public void UserPressedNumber(string numberString)
    {
        if (currentInput.Length < 4)
        {
            currentInput += numberString;
            UpdateDisplay();
        }
    }

    public void ClearInput()
    {
        currentInput = "";
        UpdateDisplay();
    }

    public void EnterCode()
    {
        if (currentInput == correctCode)
        {
            StartCoroutine(AccessGrantedRoutine());
        }
        else
        {
            StartCoroutine(AccessDeniedRoutine());
        }
    }

    void UpdateDisplay()
    {
        if (displayText == null) return;

        if (currentInput == "")
        {
            displayText.text = "_ _ _ _";
            displayText.color = Color.white;
        }
        else
        {
            displayText.text = currentInput;
        }
    }

    IEnumerator AccessGrantedRoutine()
    {
        displayText.text = "GRANTED";
        displayText.color = Color.green;

        yield return new WaitForSecondsRealtime(1f);

        if (doorToUnlock != null)
        {
            DoorInteraction doorScript = doorToUnlock.GetComponent<DoorInteraction>();
            if (doorScript != null)
            {
                doorScript.UnlockAndOpenDoor();
            }
        }

        CloseKeypad();
    }

    IEnumerator AccessDeniedRoutine()
    {
        displayText.text = "DENIED";
        displayText.color = Color.red;

        yield return new WaitForSecondsRealtime(1f);

        currentInput = "";
        UpdateDisplay();
    }

    public void CloseKeypad()
    {
        keypadPanel.SetActive(false);
        currentInput = "";
        UpdateDisplay();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerControllerScript != null)
        {
            playerControllerScript.enabled = true;
        }

        Time.timeScale = 1f;
    }
}