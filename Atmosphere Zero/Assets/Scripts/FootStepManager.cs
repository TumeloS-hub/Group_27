using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    [Header("Audio References")]
    public AudioSource audioSource;
    public AudioClip[] footstepClips; // Allows multiple footstep sounds for variety

    [Header("Timing Settings")]
    public float stepInterval = 0.5f;

    private CharacterController controller;
    private float stepTimer = 0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        // Automatically get the AudioSource on this GameObject if not assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        // Check if player is on the ground and moving
        if (controller != null && controller.isGrounded && controller.velocity.magnitude > 0.1f)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                PlayRandomFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            // Reset timer when idle so the first step plays immediately upon walking
            stepTimer = stepInterval;
        }
    }

    private void PlayRandomFootstep()
    {
        if (audioSource == null || footstepClips.Length == 0) return;

        // Pick a random audio clip to avoid repetitive sound
        int randomIndex = Random.Range(0, footstepClips.Length);
        audioSource.PlayOneShot(footstepClips[randomIndex]);
    }
}