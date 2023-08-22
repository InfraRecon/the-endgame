using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playRandomAudioClip : MonoBehaviour
{
    public AudioClip[] clips;
    public float fadeTime = 1.0f;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        PlayRandomClip();
    }

    void PlayRandomClip()
    {
        if (clips.Length == 0)
        {
            Debug.LogError("No audio clips found");
            return;
        }

        int clipIndex = Random.Range(0, clips.Length);
        StartCoroutine(FadeOut(fadeTime, () => {
            source.clip = clips[clipIndex];
            StartCoroutine(FadeIn(fadeTime));
            source.Play();
        }));

        // Invoke the PlayRandomClip method after the new audio clip's duration has elapsed
        Invoke("PlayRandomClip", source.clip.length);
    }

    IEnumerator FadeOut(float time, System.Action onComplete)
    {
        float startVolume = source.volume;

        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / time;
            yield return null;
        }

        source.Stop();
        source.volume = startVolume;

        if (onComplete != null)
        {
            onComplete();
        }
    }

    IEnumerator FadeIn(float time)
    {
        float endVolume = source.volume;
        source.volume = 0;
        source.Play();

        while (source.volume < endVolume)
        {
            source.volume += endVolume * Time.deltaTime / time;
            yield return null;
        }

        source.volume = endVolume;
    }

}
