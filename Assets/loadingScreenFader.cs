using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadingScreenFader : MonoBehaviour
{
    public Material targetMaterial; // The material you want to fade in and out
    public float fadeInDuration = 1.0f; // Duration of the fade-in effect in seconds
    public float waitDuration = 10.0f; // Duration to wait after fade-in before fade-out in seconds
    public float fadeOutDuration = 1.0f; // Duration of the fade-out effect in seconds

    private float elapsedTime = 0f;
    public bool deathIn = false;
    public bool fadingIn = false;
    public bool waitForFadeIn = false;
    public bool waitForFade = false;
    public bool fadingOut = false;

    public GameObject deathLogo;
    public GameObject gameLogo;

    public ThirdPersonCameraMovement thirdPersonCameraMovement;
    public gameCounters counter;

    private void Start()
    {
        // Make sure the material is not transparent at the beginning
        Color initialColor = targetMaterial.color;
        initialColor.a = 0f;
        targetMaterial.color = initialColor;
    }

    private void Update()
    {
        if(deathIn == true || fadingIn == true || waitForFade == true || fadingOut == true);
        {
            Fader();
        }
    }

    public void StartFade()
    {
        fadingIn = true;   
    }

    private void Fader()
    {   
        // Check if we are still fading in
        if (deathIn)
        {
            DeathIn();
        }

        if (fadingIn)
        {
            if(counter.getRevives() <= 0)
            {
                deathIn = true;
                DeathIn();
            }
            else
            {
                FadeIn();
            }
        }
        
        if(waitForFadeIn)
        {
            WaitFadeIn();
        }

        if(waitForFade)
        {
            WaitFade();
        }
        
        if(fadingOut)
        {
            // Wait for the specified duration before starting to fade out
            FadeOut();
        }
    }
    private void DeathIn()
    {
        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / fadeInDuration) * 2;
        deathLogo.SetActive(true);

        // Color currentColor = targetMaterial.color;
        // currentColor.a = Mathf.Lerp(0f, 1f, t);
        // targetMaterial.color = currentColor;

        if (t >= fadeInDuration)
        {
            fadingIn = false;
            deathIn = false;
            waitForFadeIn = true;
            fadingOut = false;
            elapsedTime = 0f;

            thirdPersonCameraMovement.enabled = false;
        }
    }

    private void FadeIn()
    {
        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / fadeInDuration) * 2;

        Color currentColor = targetMaterial.color;
        currentColor.a = Mathf.Lerp(0f, 1f, t);
        targetMaterial.color = currentColor;

        if (t >= fadeInDuration)
        {
            fadingIn = false;
            deathIn = false;
            waitForFade = true;
            fadingOut = false;
            elapsedTime = 0f;

            deathLogo.SetActive(false);
            gameLogo.SetActive(true);
            thirdPersonCameraMovement.enabled = false;
        }
    }
    
    private void WaitFadeIn()
    {
        elapsedTime += Time.deltaTime;
        //float t = Mathf.Clamp01(elapsedTime / waitDuration);

        if (elapsedTime >= waitDuration)
        {
            deathIn = false;
            fadingIn = true;
            waitForFadeIn = false;
            fadingOut = false;
            elapsedTime = 0f;
        }

        thirdPersonCameraMovement.enabled = false;
    }

    private void WaitFade()
    {
        elapsedTime += Time.deltaTime;
        //float t = Mathf.Clamp01(elapsedTime / waitDuration);

        if (elapsedTime >= waitDuration)
        {
            fadingIn = false;
            waitForFade = false;
            fadingOut = true;
            elapsedTime = 0f;
        }

        thirdPersonCameraMovement.enabled = false;
    }

    private void FadeOut()
    {
        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / fadeOutDuration) * 2;

        Color currentColor = targetMaterial.color;
        currentColor.a = Mathf.Lerp(1f, 0f, t);
        targetMaterial.color = currentColor;

        if (t >= fadeOutDuration)
        {
            // Fading out is complete, you can do additional actions here if needed.
            // For example, destroy the object, disable a script, etc.

            // In this case, I'll reset the values to prepare for the next fade-in/out cycle
            fadingIn = false;
            waitForFade = false;
            fadingOut = false;
            elapsedTime = 0f;

            gameLogo.SetActive(false);
            thirdPersonCameraMovement.enabled = true;
        }
    }
}
