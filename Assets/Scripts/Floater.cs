using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    public float randomAmplitude = 0f;

    // Position Storage Variables
    public Transform posOffsetParent;
    Vector3 tempPos = new Vector3 ();

    // Use this for initialization
    void Start () 
    {
        if(posOffsetParent == null)
        {
            posOffsetParent = transform;
        }
        // Store the starting position & rotation of the object
        posOffsetParent.position = transform.position;
    }

    // Update is called once per frame
    void Update () 
    {
        // Spin object around Y-Axis
        //transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        randomAmplitude = Random.Range(-randomAmplitude,randomAmplitude);
        amplitude += randomAmplitude; 
        // Float up/down with a Sin()
        tempPos = posOffsetParent.position;
        tempPos.y += Mathf.Sin (Time.time * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}
