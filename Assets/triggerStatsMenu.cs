using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerStatsMenu : MonoBehaviour
{
    private GameObject canvasStatsMenu;
    public List<toggleUI> toggleStats = new List<toggleUI>();
    public GameObject statsCamera;
    private bool insideTrigger = false;

    private GameObject Player;
    public Transform target;

    private gameCounters counter;

    // Start is called before the first frame update
    void Start()
    {
        statsCamera = GameObject.Find("Camera Target").transform.GetChild(0).gameObject;
        statsCamera.SetActive(false);
        canvasStatsMenu = GameObject.Find("Canvas Stat Screen");
        counter = GameObject.Find("Game Manager").GetComponent<gameCounters>();
        //5 9
        for(int i = 5; i < 10; i++)
        {
            toggleStats.Add(canvasStatsMenu.transform.GetChild(0).transform.GetChild(i).GetComponent<toggleUI>());
        }
    }

    // // Update is called once per frame
    void Update()
    {
        if(insideTrigger)
        {
            Player.transform.position = target.position;
            Player.transform.rotation = target.rotation;
        }
        
        if(insideTrigger && Input.GetKeyDown(KeyCode.C))
        {
            canvasStatsMenu.transform.GetChild(0).gameObject.SetActive(false);
            statsCamera.SetActive(false);
            insideTrigger = false;
            counter.tallySoulsAndGems();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player = other.gameObject;

            canvasStatsMenu.transform.GetChild(0).gameObject.SetActive(true);
            foreach (toggleUI s in toggleStats)
            {
                s.triggerUI();
            }
            statsCamera.SetActive(true);
            insideTrigger = true;

            counter.pretendTally();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canvasStatsMenu.transform.GetChild(0).gameObject.SetActive(false);
            statsCamera.SetActive(false);
            insideTrigger = false;
        }
    }
}
