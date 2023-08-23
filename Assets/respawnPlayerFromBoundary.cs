using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnPlayerFromBoundary : MonoBehaviour
{
    public ResetPlayer reset;
    public loadingScreenFader loading;
    private bool insideTrigger = false;

    public bool automaticReset = false;

    // Start is called before the first frame update
    void Start()
    {
        reset = GameObject.Find("ResetPosition").GetComponent<ResetPlayer>();
        loading = GameObject.Find("Loading Plane").GetComponent<loadingScreenFader>();
    }

    // Update is called once per frame
    void Update()
    {
        if(insideTrigger && !automaticReset && Input.GetKeyDown(KeyCode.C))
        {
            reset.restePlayerToHub();
            loading.StartFade();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            insideTrigger = true;
            if(automaticReset)
            {
                reset.restePlayerToHub();
                loading.StartFade();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            insideTrigger = false;
        }
    }
}
