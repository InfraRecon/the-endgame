using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnPlayerFromBoundary : MonoBehaviour
{
    public ResetPlayer reset;
    public loadingScreenFader loading;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            reset.restePlayerToHub();
            loading.StartFade();
        }
    }
}
