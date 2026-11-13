using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorInteraction : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public bool isOpen = false;

    [Header("Keypad Lock Settings")]
    public bool isLockedByKeypad = false; // Check this ONLY for the basement door
    public KeypadSystem keypadSystem;     // Drag your KeypadManager here

    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    private Coroutine _currentCoroutine;
    private bool _playerInZone = false;
    private AudioSource _audioSource;

    void Start()
    {
        _closedRotation = transform.rotation;
        _openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_playerInZone && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            // If this door is locked by a keypad, pop up the keypad instead of swinging open
            if (isLockedByKeypad && !isOpen)
            {
                if (keypadSystem != null)
                {
                    keypadSystem.OpenKeypad();
                }
                return; // Stop here
            }

            // Otherwise, open/close the door normally
            if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(ToggleDoor());
        }
    }

    // Called by KeypadSystem when the correct code is entered
    public void UnlockAndOpenDoor()
    {
        isLockedByKeypad = false; // Unlock it
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(ToggleDoor()); // Swing it open
    }

    private IEnumerator ToggleDoor()
    {
        Quaternion targetRotation = isOpen ? _closedRotation : _openRotation;
        isOpen = !isOpen;

        if (_audioSource != null && _audioSource.clip != null)
        {
            _audioSource.Play();
        }

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }

        transform.rotation = targetRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInZone = false;
        }
    }
}