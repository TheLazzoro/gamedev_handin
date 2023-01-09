using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

class PlayerCameraClass
{
    public string Name {get; set;}
    public GameObject FollowCam {get; set;}
    public GameObject BallCam {get; set;}
    public bool ballCamActive = false;
}

public class CameraSwitcher : MonoBehaviour
{
    List<PlayerCameraClass> cameraList;

    void Start()
    {
        cameraList = new List<PlayerCameraClass>();
        var allCameras = FindObjectsOfType<CinemachineVirtualCamera>();  

        foreach (CinemachineVirtualCamera camera in allCameras)
        {
            // Get the player this camera is meant for (P1/P2)
            var cameraPlayer = camera.name.Split(" ")[0];
            
            // See if a "PlayerCameraClass" instance for this player exists in the cameraList
            foreach (PlayerCameraClass camClass in cameraList)
            {
                if (camClass.Name.Equals(cameraPlayer))
                {
                    // Check if the camera's name includes "Follow" or "Ball"
                    if (camera.name.Contains("Follow"))
                    {
                        camClass.FollowCam = camera.gameObject;
                    } else {
                        camClass.BallCam = camera.gameObject;
                    }
                }
            }
            
            // If no "PlayerCameraClass" instance exists, create one!
            PlayerCameraClass newPlayerClass = new PlayerCameraClass();
            newPlayerClass.Name = cameraPlayer;

            // Check if the camera's name includes "Follow" or "Ball". The other camera type will be added on the next loop
            if (camera.name.Contains("Follow"))
            {
                newPlayerClass.FollowCam = camera.gameObject;
            } else {
                newPlayerClass.BallCam = camera.gameObject;
            }
            cameraList.Add(newPlayerClass);
        }
    }

    void Update()
    {
        // Player 1 uses Left Shift to toggle BallCam
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            foreach (PlayerCameraClass camClass in cameraList)
            {
                // Find the right PlayerCameraClass
                if (camClass.Name.Equals("P1"))
                {
                    // Flip boolean
                    camClass.ballCamActive = !camClass.ballCamActive;
                    
                    camClass.BallCam.SetActive(camClass.ballCamActive);
                    camClass.FollowCam.SetActive(!camClass.ballCamActive);
                    return;
                }
            }
        }

        // Player 2 uses Left Shift to toggle BallCam
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            foreach (PlayerCameraClass camClass in cameraList)
            {
                // Find the right PlayerCameraClass
                if (camClass.Name.Equals("P2"))
                {
                    // Flip boolean
                    camClass.ballCamActive = !camClass.ballCamActive;
                    
                    camClass.BallCam.SetActive(camClass.ballCamActive);
                    camClass.FollowCam.SetActive(!camClass.ballCamActive);
                    return;
                }
            }
        }
    }
}
