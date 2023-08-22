using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomScaleGenerator : MonoBehaviour
{
    public float scaleMin = 1;
    public float scaleMax = 1;
    // Start is called before the first frame update
    void Start()
    {
        float randomScale = Random.Range(scaleMin, scaleMax);
        transform.localScale = new Vector3(randomScale,randomScale,randomScale);
    }
}
