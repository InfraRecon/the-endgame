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

    public string faceTriggerName;

    private float timer;

    void Start()
    {
        // Start the timer with a random value between minTime and maxTime
        timer = Random.Range(minTime, maxTime);
        faceTriggerName = "IsBlinking";
    }

    void Update()
    {
        // Decrement the timer every frame
        timer -= Time.deltaTime;

        // If the timer has reached zero or less, trigger the animation and reset the timer with a new random value
        if (timer <= 0f)
        {
            animator.SetTrigger(faceTriggerName);
            timer = Random.Range(minTime, maxTime);
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Neutral Blinking") ||
        animator.GetCurrentAnimatorStateInfo(0).IsName("Scream Blinking") ||
        animator.GetCurrentAnimatorStateInfo(0).IsName("Happy Blinking"))
        {
            turnOffPupils();
        }
        else
        {
            turnONPupils();
        }
    }

    public void NeutralFace()
    {
        animator.SetFloat("Blend",1);
        faceTriggerName = "IsBlinking";
    }

    public void ScreamFace()
    {
        animator.SetFloat("Blend",2);
        faceTriggerName = "IsScreamBlinking";
    }

    public void HappyFace()
    {
        animator.SetFloat("Blend",3);
        faceTriggerName = "IsHappyBlinking";
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
