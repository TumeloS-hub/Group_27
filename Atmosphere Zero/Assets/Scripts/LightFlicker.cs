//Title: How to Make Flickering Light in Unity
//Author: Unity Unlocked
//Date: 24 October 2026
//Code Version: C#
//Availability: https://youtu.be/5K2uG7bVJAQ?si=s6ZZ9ppptRdviHpD 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light myLight;

    public float minIntensity = .5f;
    public float maxIntensity = 5.0f;
    public float flickerSpeed = 0.1f;

    private void Start()
    {
        myLight = GetComponent<Light>();

        InvokeRepeating("Flicker", 0f, flickerSpeed);
    }

    private void Flicker()
    {
        float randomIntensity = Random.Range(minIntensity, maxIntensity);
        myLight.intensity = randomIntensity;
    }
}