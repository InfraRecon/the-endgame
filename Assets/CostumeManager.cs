using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CostumeManager : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TextMeshProUGUI lockerLabel;
    public GameObject buyButton;

    public GameObject[] etherCostumeAssets; // The prefab to instantiate
    public GameObject[] stacyCostumeAssets; // The prefab to instantiate
    public GameObject[] percyCostumeAssets; // The prefab to instantiate
    public GameObject[] sunCostumeAssets; // The prefab to instantiate

    public bool etherOriginalCostume = true;
    public bool etherModernCostume = false;
    public bool percyKinCostume = false;
    public bool sunMonkCostume = false;

    public int soulReqInitial = 100;
    public int gemReqInitial = 25;

    public int soulReq = 0;
    public int gemReq = 0;
    
    public int soulReqText = 100;
    public int gemReqText = 25;

    private gameCounters counter;
    private int souls;
    private int gems;
    public TextMeshProUGUI soulText;
    public TextMeshProUGUI gemText;

    private int currentUnlockedCostume;
    private int currentSelectedCostume;

    public GameObject[][] costumeFilterArray;
    public bool[] costumeActiveArray;

    void Start()
    {
        costumeFilterArray = new GameObject[][] { etherCostumeAssets, stacyCostumeAssets, percyCostumeAssets, sunCostumeAssets };
        costumeActiveArray = new bool[] { etherOriginalCostume, etherModernCostume, percyKinCostume, sunMonkCostume };
        counter = GetComponent<gameCounters>();
    }

    public void changeCostume(int costumeNumber)
    {
        getBankedItems();
        costumeActiveArray = new bool[] { etherOriginalCostume, etherModernCostume, percyKinCostume, sunMonkCostume };
        currentSelectedCostume = costumeNumber;
        if(costumeActiveArray[costumeNumber] == false)
        {
            lockerLabel.text = "Locked";
            buyButton.SetActive(true);
        }
        else
        {
            lockerLabel.text = "Unlocked";
            buyButton.SetActive(false);
        }
        soulReq = soulReqInitial * costumeNumber;
        gemReq =  gemReqInitial * costumeNumber;

        soulText.text = (souls).ToString() + "/" + (soulReq).ToString();
        gemText.text = (gems).ToString() + "/" + (gemReq).ToString();

        for(var i = 0; i < costumeFilterArray.Length; i++)
        {
            for(var j = 0; j < costumeFilterArray[i].Length; j++)
            {
                if(i != costumeNumber)
                {
                    costumeFilterArray[i][j].SetActive(false);
                }
                else
                {
                    costumeFilterArray[i][j].SetActive(true);

                    if(costumeActiveArray[i] == true)
                    {
                        currentUnlockedCostume = i;
                    }
                }
            }
        }
    }

    public void activateCurrentCostume()
    {
        changeCostume(currentUnlockedCostume);
        dropdown.value = currentUnlockedCostume;
    }

    public void tryUnlockLockedCostume()
    {
        getBankedItems();
        if(currentSelectedCostume == 1)
        {
            unlockEtherModernCostume();
        }
        else if(currentSelectedCostume == 2)
        {
            unlockPercyKinCostume();
        }
        else if(currentSelectedCostume == 3)
        {
            unlockSunMonkCostume();
        }
        changeCostume(currentSelectedCostume);

        getBankedItems();
    }

///
    public void unlockEtherModernCostume()
    {
        bool checkCounters = unlockRequirement(souls, soulReq, gems, gemReq);
        if(checkCounters == true)
        {
            etherModernCostume = true;
            counter.updateBankedSoulEssence(-soulReq);
            counter.updateBankedGemItem(-gemReq);
        }
    }

    public void unlockPercyKinCostume()
    {
        bool checkCounters = unlockRequirement(souls, soulReq, gems, gemReq);
        if(checkCounters == true)
        {
            percyKinCostume = true;
            counter.updateBankedSoulEssence(-soulReq);
            counter.updateBankedGemItem(-gemReq);
        }
    }

    public void unlockSunMonkCostume()
    {
        bool checkCounters = unlockRequirement(souls, soulReq, gems, gemReq);
        if(checkCounters == true)
        {
            sunMonkCostume = true;
            counter.updateBankedSoulEssence(-soulReq);
            counter.updateBankedGemItem(-gemReq);
        }
    }

///

////

    public void getBankedItems()
    {
        souls = counter.getSoulBankedEssence();
        gems = counter.getGemBankedItem();
    }

    private bool unlockRequirement(int mySouls,int soulReq, int myGems, int gemReq)
    {
        if(mySouls >= soulReq && myGems >= gemReq)
        {
            return true;
        }

        return false;
    }
}
