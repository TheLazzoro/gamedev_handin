using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.enabled = false;
        Timer.OnTimerEnd += Timer_OnTimerEnd;
    }

    private void Timer_OnTimerEnd()
    {
        cam.enabled = true;
    }

    void Update()
    {
        transform.LookAt(target);
        transform.Translate(Vector3.right * Time.deltaTime * 20);
    }

    private void OnDestroy()
    {
        Timer.OnTimerEnd -= Timer_OnTimerEnd;
    }
}
