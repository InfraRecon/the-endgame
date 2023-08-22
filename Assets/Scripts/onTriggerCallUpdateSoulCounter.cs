using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onTriggerCallUpdateSoulCounter : MonoBehaviour
{
    public gameCounters counter;
    public pulseGameObject pulse;

    public int checkForlayer = 0;

    public toggleUI tUI;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 12 && checkForlayer == 12)
        {
            tUI.triggerUI();
            pulse.Pulse();
            counter.updateSoulEssence(1);
        }

        else if(other.gameObject.layer == 16 && checkForlayer == 16)
        {
            tUI.triggerUI();
            pulse.Pulse();
            counter.updateRevives(1);
        }

        else if(other.gameObject.layer == 21 && checkForlayer == 21)
        {
            tUI.triggerUI();
            pulse.Pulse();
            counter.updateGemItem(1);
        }
    }
}
