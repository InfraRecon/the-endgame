using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnvironmentSpawnManager : MonoBehaviour
{
    private GameLevelBlockManager gameLevelBlockManager;
    public int blockNumber = 1;
    // public GameObject[] environmentsToInstantiate; // The prefab to instantiate
    private Transform locationToInstantiate; // The location to instantiate the prefab
    private float dist = 60f;

    private bool processDist = false;

    private bool hasSpawned = false;

    private GameObject player;
    private int LevelNumberVar;
    public bool isStarterHub = false;
    private Transform newLevelBinParent;

    private void Start()
    {
        newLevelBinParent = GameObject.Find("Level Environment Bin").GetComponent<Transform>();
        locationToInstantiate = gameObject.transform.GetChild(0).transform;
        
        // if(!isStarterHub)
        // {
        //     LevelNumberVar = Random.Range(1,3);
        //     //1 Up, 2 Straight,3 Down

        //     if(LevelNumberVar == 1)
        //     {
        //         transform.position = new Vector3(transform.position.x,transform.position.y + 2.5f,transform.position.z);
        //         transform.rotation = Quaternion.Euler(-5,transform.eulerAngles.y,transform.eulerAngles.z);
        //         //transform.GetChild(0).transform.localPosition = new Vector3(0,-2.5f,locationToInstantiate.localPosition.z);
        //     }
        //     else if(LevelNumberVar == 2)
        //     {
        //         transform.position = new Vector3(transform.position.x,transform.position.y, transform.position.z);
        //         transform.rotation = Quaternion.Euler(0,0,0);
        //         //transform.GetChild(0).transform.localPosition = new Vector3(0,0,locationToInstantiate.localPosition.z);
        //     }
        //     else if(LevelNumberVar == 3)
        //     {
        //         transform.position = new Vector3(transform.position.x,transform.position.y-2.5f,transform.position.z);
        //         transform.rotation = Quaternion.Euler(5,transform.eulerAngles.y,transform.eulerAngles.z);
        //         //transform.GetChild(0).transform.localPosition = new Vector3(0,2.5f,locationToInstantiate.localPosition.z);
        //     }
        // }
    }

    private void FixedUpdate()
    {
        if(processDist && !isStarterHub)
        {
            dist = Vector3.Distance(player.transform.position, transform.position);

            if (dist > 400f * transform.localScale.z)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isStarterHub)
        {
            if(hasSpawned == false)
            {
                player = other.gameObject;
                GameLevelBlockManager gameLevelBlockManager = GameObject.Find("Game Manager").GetComponent<GameLevelBlockManager>();
                List<GameObject> environmentsToInstantiate = gameLevelBlockManager.GetGameObjectList(blockNumber);
                // GameObject gameEnvironment = Instantiate(gameObject, locationToInstantiate.position, locationToInstantiate.rotation);
                
                int levelNumber = gameLevelBlockManager.levelCount();
                int generateEnvNum = 0;
                if(levelNumber == 10)
                {
                    generateEnvNum = 0;
                }
                else
                {
                    generateEnvNum = Random.Range(1, environmentsToInstantiate.Count -1);
                    if(generateEnvNum > environmentsToInstantiate.Count -1)
                    {
                        generateEnvNum = environmentsToInstantiate.Count -1;
                    }
                    generateEnvNum = gameLevelBlockManager.levelEnvironmentRepeater(generateEnvNum);
                }
                // /+ environmentsToInstantiate[generateEnvNum].transform.position.y
                GameObject environment =  Instantiate(environmentsToInstantiate[generateEnvNum], new Vector3(locationToInstantiate.position.x, 
                                                                                                            locationToInstantiate.position.y, 
                                                                                                            locationToInstantiate.position.z), 
                                                                                                            environmentsToInstantiate[generateEnvNum].transform.rotation);

                environment.transform.SetParent(newLevelBinParent);
                
                processDist = true;
                hasSpawned = true;
            }
        }
    }

    public void changeBlock(int newBlockNumber)
    {
        blockNumber = newBlockNumber;
        Debug.Log("Block Chnaged");
    }

    public void spawnONCall()
    {
        if(isStarterHub)
        {
            GameLevelBlockManager gameLevelBlockManager = GameObject.Find("Game Manager").GetComponent<GameLevelBlockManager>();
            List<GameObject> environmentsToInstantiate = gameLevelBlockManager.GetGameObjectList(blockNumber);
            // GameObject gameEnvironment = Instantiate(gameObject, locationToInstantiate.position, locationToInstantiate.rotation);
            
            int levelNumber = gameLevelBlockManager.levelCount();
            int generateEnvNum = 0;
            if(levelNumber == 10)
            {
                generateEnvNum = 0;
            }
            else
            {
                generateEnvNum = Random.Range(1, environmentsToInstantiate.Count -1);
                if(generateEnvNum > environmentsToInstantiate.Count -1)
                {
                    generateEnvNum = environmentsToInstantiate.Count -1;
                }
                generateEnvNum = gameLevelBlockManager.levelEnvironmentRepeater(generateEnvNum);
            }
            // /+ environmentsToInstantiate[generateEnvNum].transform.position.y
            GameObject environment =  Instantiate(environmentsToInstantiate[generateEnvNum], new Vector3(locationToInstantiate.position.x, 
                                                                                                        locationToInstantiate.position.y, 
                                                                                                        locationToInstantiate.position.z), 
                                                                                                        environmentsToInstantiate[generateEnvNum].transform.rotation);
            
            Transform newParent = GameObject.Find("Starter Environment Bin").GetComponent<Transform>();

            foreach (Transform child in newParent) 
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in newLevelBinParent) 
            {
                Destroy(child.gameObject);
            }


            environment.transform.SetParent(newParent);
            processDist = true;
        }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         Destroy(gameObject);
    //     }
    // }
}

