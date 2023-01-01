using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera camera;
    private CinemachineBasicMultiChannelPerlin perlin;
    
    private float startingIntensity;
    private float currentIntensity;

    private void Awake() {
        camera = GetComponent<CinemachineVirtualCamera>();
        perlin = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float intensity, float frequency) {
        startingIntensity = intensity;
        currentIntensity = intensity;

        perlin.m_AmplitudeGain = intensity;
        perlin.m_FrequencyGain = frequency;
    }

    private void Update() {
        if (currentIntensity > 0) {
            currentIntensity -= startingIntensity/1000;
            perlin.m_AmplitudeGain = currentIntensity;
        }
    }
}
