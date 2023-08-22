using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationManager : MonoBehaviour
{
    public AnimationCurve curve;
    public string parameterName;
    public float speed = 1f;

    private Animator animator;
    private float startTime;
    private float offset;

    private void Start()
    {
        animator = GetComponent<Animator>();
        offset = Random.Range(0f, 100f); // Generate a random offset for each instance
    }

    private void Update()
    {
        float noiseValue = Mathf.PerlinNoise(0,offset);

        animator.SetFloat(parameterName, noiseValue);
    }
}
