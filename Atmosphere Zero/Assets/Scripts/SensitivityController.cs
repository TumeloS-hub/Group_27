using UnityEngine;
using UnityEngine.UI;

public class SensitivityController : MonoBehaviour
{
    [Header("UI Reference")]
    public Slider sensitivitySlider;

    [Header("Sensitivity Settings")]
    public float maxSensitivityMultiplier = 0.2f; 

    private void Start()
    {
        if (sensitivitySlider != null)
        {
            float savedSens = PlayerPrefs.GetFloat("MouseSensitivity", 0.1f);
            sensitivitySlider.value = savedSens;
            SetSensitivity(savedSens);
        }
    }

    public void SetSensitivity(float sliderValue)
    {
        // Scale down for smooth sensitivity
        float finalSensitivity = sliderValue * maxSensitivityMultiplier;

        PlayerPrefs.SetFloat("MouseSensitivity", sliderValue);

        FPController player = FindAnyObjectByType<FPController>();
        if (player != null)
        {
            player.mouseSensitivity = finalSensitivity;
        }
    }
}