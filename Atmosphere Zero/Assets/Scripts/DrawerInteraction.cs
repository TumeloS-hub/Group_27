using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrawerInteraction : MonoBehaviour
{
    public float slideDistance = 0.4f; // how far drawer pulls out
    public float slideSpeed = 3f;
    public bool isOpen = false;

    private Vector3 _closedPosition;
    private Vector3 _openPosition;
    private Coroutine _currentCoroutine;
    private bool _playerInZone = false;
    private AudioSource _audioSource;

    void Start()
    {
        _closedPosition = transform.localPosition;
        // Slides along local Z axis 
        _openPosition = _closedPosition + new Vector3(0, 0, slideDistance);
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_playerInZone && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(ToggleDrawer());
        }
    }

    private IEnumerator ToggleDrawer()
    {
        Vector3 targetPosition = isOpen ? _closedPosition : _openPosition;
        isOpen = !isOpen;

        if (_audioSource != null && _audioSource.clip != null)
        {
            _audioSource.Play();
        }

        while (Vector3.Distance(transform.localPosition, targetPosition) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * slideSpeed);
            yield return null;
        }

        transform.localPosition = targetPosition;
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