using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnPlayerFromBoundary : MonoBehaviour
{
    public ResetPlayer reset;
    private ResetPlayer resetToDesert;
    private loadingScreenFader loading;
    private bool insideTrigger = false;

    public bool automaticReset = false;

    // Start is called before the first frame update
    void Start()
    {
        if(reset == null)
        {
            reset = GameObject.Find("ResetPosition").GetComponent<ResetPlayer>();
        }
        resetToDesert = GameObject.Find("DesertPosition").GetComponent<ResetPlayer>();
        loading = GameObject.Find("Loading Plane").GetComponent<loadingScreenFader>();
    }

    // Update is called once per frame
    void Update()
    {
        if(insideTrigger && !automaticReset && Input.GetKeyDown(KeyCode.C) ||
        insideTrigger && !automaticReset && Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            reset.resetPlayerToHub();
            loading.StartFade();
        }
    }

    public void ResetToDesert()
    {
        resetToDesert.resetPlayerToHub();
        loading.StartFade();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            insideTrigger = true;
            if(automaticReset)
            {
                reset.resetPlayerToHub();
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
