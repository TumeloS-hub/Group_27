//Title: How to Open and Close Door in Unity | Step-by-Step Tutorial 2024
//Author: Martin Dev
//Date: 25 October 2026
//Code Version: 1.0
//Availability: https://www.youtube.com/watch?v=smlgtS07jaQ

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorInteraction : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public bool isOpen = false;

    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    private Coroutine _currentCoroutine;
    private bool _playerInZone = false;
    private AudioSource _audioSource; // Reference for the sound

    void Start()
    {
        _closedRotation = transform.rotation;
        _openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));

        // Automatically get the Audio Source component attached to this door
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_playerInZone && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(ToggleDoor());
        }
    }

    private IEnumerator ToggleDoor()
    {
        Quaternion targetRotation = isOpen ? _closedRotation : _openRotation;
        isOpen = !isOpen;

        // Play the door sound effect as soon as movement starts
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