using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeAnimator : MonoBehaviour
{
    public Animator animator;

    public float minTime = 1f;
    public float maxTime = 5f;

    public GameObject pupilLeft;
    public GameObject pupilRight;

    private float timer;

    void Start()
    {
        // Start the timer with a random value between minTime and maxTime
        timer = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        // Decrement the timer every frame
        timer -= Time.deltaTime;

        // If the timer has reached zero or less, trigger the animation and reset the timer with a new random value
        if (timer <= 0f)
        {
            animator.SetTrigger("IsBlinking");
            timer = Random.Range(minTime, maxTime);
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Neutral Blinking"))
        {
            turnOffPupils();
        }
        else
        {
            turnONPupils();
        }
    }

    void NeutralFace()
    {

    }

    void ScreamFace()
    {

    }
    void turnONPupils()
    {
        pupilLeft.SetActive(true);
        pupilRight.SetActive(true);
    }

    void turnOffPupils()
    {
        pupilLeft.SetActive(false);
        pupilRight.SetActive(false);
    }
}
