using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAudioSource : MonoBehaviour
{
public AudioSource audioSource;
    public float maxVolumeDistance = 10f; // Adjust this value as needed
    private GameObject player;
    public Transform StartArea; // The specific transform to check distance against
    public AudioClip[] nearAudioClips; // Audio clips for when player is near the transform
    public AudioClip[] farAudioClips; // Audio clips for when player is far from the transform
    private bool isNearTransform = false; // Flag to track player's proximity to the transform

    private playRandomAudioClip masterSource;

    private void Start()
    {
        player = GameObject.Find("Ghost Player");
        masterSource = GameObject.Find("Game Manager").GetComponent<playRandomAudioClip>();
    }

    private void Update()
    {
        float distanceToTransform = Vector3.Distance(player.transform.position, StartArea.position);

        if (distanceToTransform <= maxVolumeDistance)
        {
            if (!isNearTransform)
            {
                isNearTransform = true;
                SwitchAudioClips(nearAudioClips);
            }
        }
        else
        {
            if (isNearTransform)
            {
                isNearTransform = false;
                SwitchAudioClips(farAudioClips);
            }
        }

        //PlayAudioWithAdjustedVolume(player.transform.position);

        audioSource.volume = masterSource.GetAudioVolume();
    }

    private void SwitchAudioClips(AudioClip[] newClips)
    {
        if (audioSource != null && newClips.Length > 0)
        {
            audioSource.clip = newClips[Random.Range(0, newClips.Length)];
            audioSource.Play();
            audioSource.volume = masterSource.GetAudioVolume();
        }
    }

    // private void PlayAudioWithAdjustedVolume(Vector3 listenerPosition)
    // {
    //     if (audioSource != null)
    //     {
    //         float distanceToListener = Vector3.Distance(listenerPosition, transform.position);

    //         if (distanceToListener > maxVolumeDistance || !isNearTransform)
    //         {
    //             audioSource.volume = masterSource.GetAudioVolume();
    //             audioSource.enabled = true;
    //         }
    //         else
    //         {
    //             audioSource.enabled = true;
    //             audioSource.volume = masterSource.GetAudioVolume();
    //         }
    //     }
    // }
}
