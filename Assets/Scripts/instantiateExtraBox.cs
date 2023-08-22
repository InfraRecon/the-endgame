using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateExtraBox : MonoBehaviour
{
    public GameObject[] boxPrefabs;
    public GameObject extraBox;
    private int randomBoxNum;
    private bool randomSpawnExtra = false;
    // Start is called before the first frame update
    void Start()
    {
        randomBoxNum = Random.Range(0,boxPrefabs.Length);

        int intantiateBoxNum = Random.Range(-1,2);
        if(intantiateBoxNum == 1)
        {
            randomSpawnExtra = true;
        }

        if(randomSpawnExtra)
        {
            extraBox = Instantiate(boxPrefabs[randomBoxNum], new Vector3(transform.position.x,transform.position.y + 1.9f,transform.position.z), Quaternion.identity);
            extraBox.transform.parent = transform.parent;
        }
    }
}

