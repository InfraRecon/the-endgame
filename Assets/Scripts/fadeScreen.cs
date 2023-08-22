using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeScreen : MonoBehaviour
{
    public Material material;
    // public float fadeDuration = 1f;
    // private float currentFade = 0f;

    public float fadeInDuration = 1f;
    public float waitDuration = 1f;
    public float fadeOutDuration = 1f;

    public GameObject gameLogo;

    // public void FadeOut()
    // {
    //     StartCoroutine(FadeTo(0f));
    // }

    // public void FadeIn()
    // {
    //     StartCoroutine(FadeTo(1f));
    // }

    // IEnumerator FadeTo(float targetFade)
    // {
    //     float startFade = currentFade;
    //     float timer = 0f;

    //     while (timer < fadeDuration)
    //     {
    //         currentFade = Mathf.Lerp(startFade, targetFade, timer / fadeDuration);
    //         material.SetColor("_BaseColor", new Color(0,0,0,currentFade));
    //         yield return null;
    //         timer += Time.deltaTime;
    //     }

    //     currentFade = targetFade;
    //     material.SetColor("_BaseColor", new Color(0,0,0,currentFade));
    // }

    // void OnValidate()
    // {
    //     if (material != null)
    //     {
    //         material.SetColor("_BaseColor", new Color(0,0,0,currentFade));
    //     }
    // }

    public void fadeInandThenOut()
    {
        StartCoroutine(FadeInAndWait());
    }

    IEnumerator FadeInAndWait()
    {
        float timer = 0f;

        while (timer < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeInDuration);
            material.SetColor("_BaseColor", new Color(0,0,0,alpha));
            yield return null;
            timer += Time.deltaTime;

            if(timer >= fadeInDuration)
            {
                material.SetColor("_BaseColor", new Color(0,0,0,1));
                gameLogo.SetActive(true);
            }
        }

        yield return new WaitForSeconds(waitDuration);

        timer = 0f;

        while (timer < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeOutDuration);
            material.SetColor("_BaseColor", new Color(0,0,0,alpha));
            yield return null;
            timer += Time.deltaTime;
        }

        material.SetColor("_BaseColor", new Color(0,0,0,0f));
        gameLogo.SetActive(false);
    }
}
