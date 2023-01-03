using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using Cinemachine;

public class Goal : MonoBehaviour
{
    [SerializeField] string teamName;
    [SerializeField] GameObject yeetArea;
    private CarYeeter carYeeter;
    private List<Rigidbody> affectedObjects;

    GameObject agent;
    bool activated;
    float pauseTime = 3f;

    private static bool doSlowMotion;
    private static readonly float SlowMotionTime = 1;
    private static float SlowMotionTime_Left;
    private static float SlowDownFactor = 0.2f;
    private static float FixedDeltaTime;

    void Start()
    {
        agent = GetComponent<GameObject>();
        activated = true;
        FixedDeltaTime = Time.fixedDeltaTime;
        carYeeter = yeetArea.GetComponent<CarYeeter>();
    }

    void Update()
    {   
        if (activated == false) {
        pauseTime -= Time.deltaTime;
            if (pauseTime <= 0) {
                RestartScene restartscene = new RestartScene();
                restartscene.ResetScene();
            }
        }

        if (doSlowMotion)
        {
            if (SlowMotionTime_Left < 0)
            {
                doSlowMotion = false;
                Time.timeScale = 1;
                Time.fixedDeltaTime = FixedDeltaTime;
            }
            else
            {
                SlowMotionTime_Left -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!activated || other.tag != "Ball") return;
        var ball = other.GetComponent<Transform>();
        activated = false; // TODO: Remember to re-activate the goal on round reset.
        Debug.Log("Scored a goal on " + teamName);
        FindObjectOfType<ScoreManager>().AddPointToTeam(gameObject.tag);
        FindObjectOfType<SFXHandler>().playSFX("goalAirhorn", 1f);

        doSlowMotion = true;
        SlowMotionTime_Left = SlowMotionTime;
        Time.timeScale = SlowDownFactor;
        Time.fixedDeltaTime = SlowDownFactor * 0.02f;

        // Explosion
        var ballEx = other.GetComponent<Explosion>();
        ballEx.TriggerExplosion();
        carYeeter.Yeet();

        // Find the actual ball and hide that (keep particles)
        GameObject.Find("Soccer Ball").SetActive(false);

        // Activate Screen Shake
        var cameras = FindObjectsOfType<CinemachineVirtualCamera>();
        foreach (CinemachineVirtualCamera camera in cameras) {
            camera.GetComponent<CameraShake>().Shake(15f, 0.08f);
        }
    }
}
