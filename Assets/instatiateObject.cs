using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instatiateObject : MonoBehaviour
{
    public GameObject levelObject;
    public Transform locationPos;
    private Transform newLevelBinParent;

    // Start is called before the first frame update
    private void Start()
    {
        newLevelBinParent = GameObject.Find("Level Environment Bin").GetComponent<Transform>();
        GameObject environment = Instantiate(levelObject, locationPos.position, Quaternion.identity);
        environment.transform.SetParent(newLevelBinParent);
    }
}
