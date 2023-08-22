using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameObjectScaleReducer : MonoBehaviour
{
    public float scaleDownSpeed = 5.0f;

    public float minSize = 0f;
    public float maxSize = 1f;
    public float startSize = 1f;
    
    private Vector3 targetScale;
    private Vector3 baseScale;
    private float currScale;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp (transform.localScale, targetScale, scaleDownSpeed * Time.deltaTime);
        currScale--;
        currScale = Mathf.Clamp (currScale, minSize, maxSize+1f);
        
        targetScale = baseScale * currScale;
    }
}
