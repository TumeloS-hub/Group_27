//Graphics Quality Controller
//Adapted from Unity YouTube Tutorial (How To Limit Frame Rate In Unity 6)
//C#
//https://www.youtube.com/watch?v=GB29WWBy4_s

using UnityEngine;
using TMPro;

public class GraphicsController : MonoBehaviour
{
    [Header("UI Reference")]
    public TMP_Dropdown graphicsDropdown;

    private void Start()
    {
        if (graphicsDropdown != null)
        {
            int savedQuality = PlayerPrefs.GetInt("GraphicsQuality", 1);
            graphicsDropdown.value = savedQuality;
            ChangeGraphicsQuality();
        }
    }

    public void ChangeGraphicsQuality()
    {
        int qualityIndex = graphicsDropdown.value;
        QualitySettings.SetQualityLevel(qualityIndex);

        // Low, Medium, and High performance tiers
        if (qualityIndex == 0) // Low
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 30;
        }
        else if (qualityIndex == 1) // Medium
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
        else if (qualityIndex == 2) // High
        {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = -1; // Uncapped / monitor refresh rate
        }

        PlayerPrefs.SetInt("GraphicsQuality", qualityIndex);
        Debug.Log("Graphics quality tier changed to: " + qualityIndex + " | Target FPS set.");
    }
}