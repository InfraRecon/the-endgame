using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playRandomAudioClip : MonoBehaviour
{
    public float AudioVolume = 0.5f;

    public AudioSource MasterAudioSource;

    public void ChangeAudioVolume()
    {
        AudioVolume = MasterAudioSource.volume;
    }

    public float GetAudioVolume()
    {
        return AudioVolume;
    }
}
