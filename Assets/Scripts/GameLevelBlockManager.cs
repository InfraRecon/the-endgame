using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelBlockManager : MonoBehaviour
{
    public GameObject[] forestEnvironmentToInstantiate; // The prefab to instantiate
    public GameObject[] desertEnvironmentToInstantiate; // The prefab to instantiate
    public GameObject[] modernEnvironmentToInstantiate; // The prefab to instantiate
    public int previousLevelNumber = 0;
    public int currentLevelNumber = 0;
    public int counter = 0;

    List<GameObject> newList;
    // Start is called before the first frame update

    public List<GameObject> GetGameObjectList(int blockNum)
    {
        if(blockNum == 1)
        {
            newList = new List<GameObject>(forestEnvironmentToInstantiate);
        }
        else if(blockNum == 2)
        {
            newList = new List<GameObject>(desertEnvironmentToInstantiate);
        }
        else if(blockNum == 3)
        {
            newList = new List<GameObject>(modernEnvironmentToInstantiate);
        }

        return newList;
    }

    public int levelEnvironmentRepeater(int levelNum)
    {
        while(levelNum == previousLevelNumber)
        {
            levelNum = Random.Range(1, newList.Count);
            if(levelNum > newList.Count -1)
            {
                levelNum = newList.Count - 1;
            }
        }

        previousLevelNumber = levelNum;

        return levelNum;
    }

    public int levelCount()
    {
        if(counter == 11)
        {
            counter = 0;
        }
        else
        {
            counter++;
        }
        return counter;
    }
}
