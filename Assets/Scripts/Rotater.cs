using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    public bool rotationOnXAxis = false;
    public bool rotationOnYAxis = true;
    public bool rotationOnZAxis = false;
    public float rotationsPerMinute = 10.0f;

    private float currentAngle = 0.0f;

    public bool rotateAndStop = false;
    public bool isRotating = false;

    private int spinCount = 0;

    public bool randomRotate = false;
    public bool rotationReset = false;
    
    void Start()
    {
        if(randomRotate)
        {
            rotationsPerMinute = Random.Range(0, rotationsPerMinute);
        }
    }

    void Update()
    {
        if(rotateAndStop)
        {
            if (isRotating)
            {
                currentAngle = transform.localRotation.eulerAngles.y; 
                // float desiredEndAngle = currentAngle + 360f;
                currentAngle += 6.0f * rotationsPerMinute * Time.deltaTime;

                if(rotationOnXAxis)
                {
                    transform.rotation = Quaternion.Euler(currentAngle, 0f, 0.0f);
                }

                if(rotationOnYAxis)
                {
                    transform.rotation = Quaternion.Euler(0.0f, currentAngle, 0.0f);
                }

                if(rotationOnZAxis)
                {
                    transform.rotation = Quaternion.Euler(0.0f, 0f, currentAngle);
                }

                if (currentAngle >= 360f)
                {
                    // isRotating = false;
                    // currentAngle = 0.0f;
                    spinCount += 1;
                }

                if(spinCount == 5)
                {
                    isRotating = false;
                    currentAngle = 0.0f;
                    spinCount = 0;
                    rotationReset = true;
                }
            }
        }
        else
        {
            if(rotationOnXAxis)
            {
                transform.Rotate(6.0f * rotationsPerMinute * Time.deltaTime, 0, 0);
            }

            if(rotationOnYAxis)
            {
                transform.Rotate(0, 6.0f * rotationsPerMinute * Time.deltaTime, 0);
            }

            if(rotationOnZAxis)
            {
                transform.Rotate(0, 0, 6.0f * rotationsPerMinute * Time.deltaTime);
            }
        }

        resetRotation();
    }

    public void quickRotate()
    {
        isRotating = true;
    }

    public void resetRotation()
    {
        if(rotationReset)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
            rotationReset = false;
        }
    }
}
