using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatePlayerObject : MonoBehaviour
{
    public float rotationTime = 2f;
    public float rotationSpeed = 50f;

    private Quaternion initialRotation;
    private Quaternion finalRotation;
    private float elapsedTime = 0f;
    private bool isRotating = false;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        if (isRotating)
        {
            if (Quaternion.Angle(transform.rotation, initialRotation) > 0.1f)
            {
                transform.rotation = initialRotation;
            }
        }
    }

    public void RotateAndReset()
    {
        if (!isRotating)
        {
            isRotating = true;
            StartCoroutine(RotateCoroutine());
        }
    }

    private IEnumerator RotateCoroutine()
    {
        finalRotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, 360f, 0f));
        while (elapsedTime < rotationTime)
        {
            float rotationAngle = rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, rotationAngle, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = initialRotation;
        elapsedTime = 0f;
        isRotating = false;
    }
}
