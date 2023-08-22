using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonLevelTrigger : MonoBehaviour
{
    public int levelEnvironmentNumber = 1;
    private EnvironmentSpawnManager environmentSpawnManager;
    private bool playerOnTrigger = false;
    private GameObject Player;
    public Transform target;

    private GameObject canvasLevelSelectScreen;

    private toggleUI uiInteract;

    void Start()
    {
        uiInteract = GameObject.Find("Button Prompts").GetComponent<toggleUI>();
        canvasLevelSelectScreen = GameObject.Find("Canvas Level Select Screen");
        canvasLevelSelectScreen.SetActive(false);
    }

    void Update()
    {
        if(playerOnTrigger)
        {
            uiInteract.triggerUI();
            if(Input.GetKeyDown(KeyCode.C) && !canvasLevelSelectScreen.activeSelf)
            {
                canvasLevelSelectScreen.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.C) && canvasLevelSelectScreen.activeSelf)
            {
                canvasLevelSelectScreen.SetActive(false);
            }

            if(canvasLevelSelectScreen.activeSelf)
            {
                Player.transform.position = target.position;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        environmentSpawnManager = transform.parent.transform.parent.GetComponent<EnvironmentSpawnManager>();
        
        if(other.CompareTag("Player"))
        {
            //other.gameObject.transform.SetParent(transform);
            playerOnTrigger = true;
            Player = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //other.gameObject.transform.SetParent(null);
            playerOnTrigger = false;
        }
    }
}
