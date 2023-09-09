using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraOffsetManager : MonoBehaviour
{
    public CinemachineFollowZoom virtualCameraClassic;
    public CinemachineFollowZoom virtualCameraEnhanced;
    public CinemachineCameraOffset[] cameraOffsetExtensions;

    public Vector3 offset;
    public float damping = 5f;

    private void Start()
    {
        // if (virtualCamera == null)
        // {
        //     //virtualCamera = GetComponent<CinemachineVirtualCamera>();
        //     if (virtualCamera == null)
        //     {
        //         Debug.LogError("CinemachineVirtualCamera component not found.");
        //         return;
        //     }
        // }

        // Get the Cinemachine Camera Offset extension
        //cameraOffsetExtension = virtualCamera.GetCinemachineComponent<CinemachineCameraOffset>();
    }

    private void Update()
    {
        // Check for user input or other conditions to change the camera offset
            if(virtualCameraClassic.gameObject.activeSelf)
            {
                if (Input.GetKey(KeyCode.JoystickButton2))
                {
                    // Smoothly interpolate the FOV values
                    virtualCameraClassic.m_MinFOV = Mathf.Lerp(virtualCameraClassic.m_MinFOV, 60f, Time.deltaTime * damping);
                    virtualCameraClassic.m_MaxFOV = Mathf.Lerp(virtualCameraClassic.m_MaxFOV, 60f, Time.deltaTime * damping);
                }
                else
                {
                    // Smoothly interpolate the FOV values
                    virtualCameraClassic.m_MinFOV = Mathf.Lerp(virtualCameraClassic.m_MinFOV, 70f, Time.deltaTime * damping);
                    virtualCameraClassic.m_MaxFOV = Mathf.Lerp(virtualCameraClassic.m_MaxFOV, 70f, Time.deltaTime * damping);
                }
            }
            
            if(virtualCameraEnhanced.gameObject.activeSelf)
            {
                if (Input.GetKey(KeyCode.JoystickButton2))
                {
                    // Smoothly interpolate the FOV values
                    virtualCameraEnhanced.m_MinFOV = Mathf.Lerp(virtualCameraEnhanced.m_MinFOV, 60f, Time.deltaTime * damping);
                    virtualCameraEnhanced.m_MaxFOV = Mathf.Lerp(virtualCameraEnhanced.m_MaxFOV, 60f, Time.deltaTime * damping);
                }
                else
                {
                    // Smoothly interpolate the FOV values
                    virtualCameraEnhanced.m_MinFOV = Mathf.Lerp(virtualCameraEnhanced.m_MinFOV, 70f, Time.deltaTime * damping);
                    virtualCameraEnhanced.m_MaxFOV = Mathf.Lerp(virtualCameraEnhanced.m_MaxFOV, 70f, Time.deltaTime * damping);
                }
            }
    }

    public void SetCameraOffset(float newOffsetY)
    {
        Vector3 newOffset = new Vector3(0,newOffsetY,0);
        if (cameraOffsetExtensions != null)
        {
            // Set the new camera offset
            foreach (CinemachineCameraOffset cameraOffsetExtension in cameraOffsetExtensions) //the line the error is pointing to
	        {
                cameraOffsetExtension.m_Offset = newOffset;
            }
        }
        else
        {
            Debug.LogError("CinemachineCameraOffset component not found.");
        }
    }
}

