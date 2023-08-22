using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingLight : MonoBehaviour
{
    public Transform centerPoint;

    public float duration = 1.0f;

    public float radius = 1f;
    public float speed = 1.0f;

    private float angle = 0.0f;

    void Update()
    {
        angle += speed * Time.deltaTime;

        float x = Mathf.Sin(radius) * Mathf.Sin(angle);
        float y = Mathf.Sin(radius) * Mathf.Cos(angle);

        Vector3 offset = new Vector3(x, y, 0f);

        transform.position = centerPoint.position + offset;
    }
}
