using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();

    public AudioSource walkingAudioSource;
    public ThirdPersonCameraMovement thirdPersonCameraMovement;
    public Transform rotator;
    public AudioSource attackAudioSource;

    public AudioSource jumpAudioSource;

    public void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow) && thirdPersonCameraMovement.speed > 14f && thirdPersonCameraMovement.isGrounded || 
           Input.GetKey(KeyCode.DownArrow) && thirdPersonCameraMovement.speed > 14f && thirdPersonCameraMovement.isGrounded || 
           Input.GetKey(KeyCode.LeftArrow) && thirdPersonCameraMovement.speed > 14f && thirdPersonCameraMovement.isGrounded || 
           Input.GetKey(KeyCode.RightArrow) && thirdPersonCameraMovement.speed > 14f && thirdPersonCameraMovement.isGrounded)
        {
            if(walkingAudioSource.clip == audioClips[0])
            {
                if(!walkingAudioSource.isPlaying)
                {
                    walkingAudioSource.Play();
                }
            }
            else
            {
                walkingAudioSource.clip = audioClips[0];
                walkingAudioSource.Play();
            }
        }
        
        if(thirdPersonCameraMovement.speed < 14f || thirdPersonCameraMovement.isGrounded == false)
        {
            walkingAudioSource.Pause();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            if(attackAudioSource.clip == audioClips[1])
            {
                if(!attackAudioSource.isPlaying)
                {
                    attackAudioSource.pitch = Random.Range(0.5f,1f);
                    attackAudioSource.Play();
                }
            }   
            else
            {
                attackAudioSource.clip = audioClips[1];
            }
        }

        // if(rotator.rotation.y != 0)
        // {
        //     attackAudioSource.loop = true;
        // }
        // else
        // {
        //     attackAudioSource.loop = false;
        // }
    }

    public void JumpAudio()
    {
        jumpAudioSource.pitch = Random.Range(0.9f,1.1f);
        jumpAudioSource.Play();
    }
}
