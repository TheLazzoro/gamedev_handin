using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXHandler : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
                
    }

    void Update()
    {
        
    }

    public void playSFX(string clipName, float volume)
    {
        audioSource.PlayOneShot((AudioClip)Resources.Load("SFX/" + clipName), volume);
    }
}
