using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class Goal : MonoBehaviour
{
    [SerializeField] string teamName;
    [SerializeField] float OnScore_explosionRadius = 300f;
    [SerializeField] float OnScore_force = 7000f;
    GameObject agent;
    bool activated;
    float pauseTime = 3f;

    private static readonly float SlowMotionTime = 1;
    private static float SlowMotionTime_Left;
    private static float SlowDownFactor = 0.2f;
    private static float FixedDeltaTime;

    void Start()
    {
        agent = GetComponent<GameObject>();
        activated = true;
        FixedDeltaTime = Time.fixedDeltaTime;
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

        if (SlowMotionTime_Left < 0)
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = FixedDeltaTime;
        }
        else
            SlowMotionTime_Left -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!activated || other.tag != "Ball") return;
        var ball = other.GetComponent<Transform>();
        activated = false; // TODO: Remember to re-activate the goal on round reset.
        Debug.Log("Scored a goal on " + teamName);
        FindObjectOfType<ScoreManager>().AddPointToTeam(gameObject.tag);
        FindObjectOfType<SFXHandler>().playSFX("applause", 0.5f);

        SlowMotionTime_Left = SlowMotionTime;
        Time.timeScale = SlowDownFactor;
        Time.fixedDeltaTime = SlowDownFactor * 0.02f;

        // Explosion
        var ballEx = other.GetComponent<Explosion>();
        ballEx.TriggerExplosion();
        var affectedObjects = Physics.OverlapSphere(transform.position, OnScore_explosionRadius);
        foreach (var obj in affectedObjects)
        {
            var rigidBody = obj.GetComponent<Rigidbody>();
            if (rigidBody == null || obj.tag == "Ball")
                continue;

            rigidBody.AddExplosionForce(OnScore_force, ball.position, OnScore_explosionRadius);
            Debug.Log(rigidBody.ToString());
        }

        ball.gameObject.SetActive(false);
    }
}
