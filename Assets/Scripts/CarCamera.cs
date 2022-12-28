using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCamera : MonoBehaviour
{
    private Camera cam;


    void Start()
    {
        cam = GetComponent<Camera>();   
        cam.enabled = true;
        Timer.OnTimerEnd += Timer_OnTimerEnd;
    }

    private void Timer_OnTimerEnd()
    {
        cam.enabled = false;
    }

    private void OnDestroy()
    {
        Timer.OnTimerEnd -= Timer_OnTimerEnd;
    }
}
