using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameCounters : MonoBehaviour
{
    [SerializeField] private int soulEssence = 0;
    public TextMeshProUGUI soulEssenseText;

    [SerializeField] private int gemItem = 0;
    public TextMeshProUGUI gemItemText;

    [SerializeField] private int boxesDestroyed = 0;
    public TextMeshProUGUI boxesDestroyedText;

    [SerializeField] private int revives = 0;
    public TextMeshProUGUI revivesText;
    ///

    [SerializeField] private int bankedSoulEssence = 0;
    public TextMeshProUGUI bankedSoulEssenceText;

    [SerializeField] private int bankedBoxDestroyed = 0;
    public TextMeshProUGUI bankedBoxDestroyedText;

    [SerializeField] private int bankedGemItems = 0;
    public TextMeshProUGUI bankedGemItemsText;

    [SerializeField] private int boxMultiplier = 0;
    [SerializeField] private int reviveMultiplier = 0;

    public GameObject hitCam;
    public GameObject[] flickerEffects;
    private bool waiting = false;

    private ThirdPersonCameraMovement thirdPersonCameraMovement;
    private bool playerIsDead = false;
    public ResetPlayer reset;
    public loadingScreenFader loadingScreen;

    private void Start()
    {
        thirdPersonCameraMovement = GameObject.Find("Ghost Player").GetComponent<ThirdPersonCameraMovement>();
        revivesText.text = revives.ToString();
    }

    public void updateSoulEssence(int change)
    {
        soulEssence += change;
        soulEssenseText.text = soulEssence.ToString();
    }

    public void updateGemItem(int change)
    {
        gemItem += change;
        gemItemText.text = gemItem.ToString();
    }

    public void updateBoxesDestroyed(int change)
    {
        boxesDestroyed += change;
        boxesDestroyedText.text = boxesDestroyed.ToString();
    }

    public void updateRevives(int change)
    {        
        if(revives > 0)
        {
            if(!waiting)
            {
                waiting = true;
                StartCoroutine(WaitingCoroutine(5f));
                int currentLives = revives;
                revives += change;

                if(revives < currentLives)
                {
                    hitCam.SetActive(true);
                    
                    for(int i = 0; i < flickerEffects.Length; i++)
                    {
                        flickerGameObjectMaterial flickerEffect;
                        flickerEffect = flickerEffects[i].GetComponent<flickerGameObjectMaterial>();
                        flickerEffect.StartFlickerEffect();
                    }
                    StartCoroutine(ExampleCoroutine());
                }
                thirdPersonCameraMovement.Jump(true);
            }
        }

        if(!playerIsDead && revives <= 0)
        {
            playerIsDead = true;
            thirdPersonCameraMovement.playerDead(true);
            thirdPersonCameraMovement.enabled = false;
            StartCoroutine(WaitingCoroutine(5f));
        }

        revivesText.text = revives.ToString();
    }

    public void resetRevives()
    {
        StartCoroutine(ResetWaitCoroutine(3f));
    }

    IEnumerator ResetWaitCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        revives = 3;
        revivesText.text = revives.ToString();
        playerIsDead = false;
    }

    IEnumerator WaitingCoroutine(float waitTime)
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(waitTime);
        if(playerIsDead)
        {
            loadingScreen.StartFade();
            reset.restePlayerToHub();
            resetRevives();
        }
        waiting = false;

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.2f);
        hitCam.SetActive(false);
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
/////
    public int getSoulEssence()
    {
        return soulEssence;
    }

    public int getGemItem()
    {
        return gemItem;
    }

    public int getBoxesDestroyed()
    {
        return boxesDestroyed;
    }

    public int getRevives()
    {
        return revives;
    }

    /////
    float defaultSoulEssence = 0;
    float defaultBoxDestroyed = 0;
    float defaultGemItem = 0;

    public void tallySoulsAndGems()
    {
        bankedSoulEssence = bankedSoulEssence + (soulEssence) * revives;
        bankedBoxDestroyed = bankedBoxDestroyed + boxesDestroyed;
        bankedGemItems = bankedGemItems + gemItem;

        soulEssence = 0;
        updateSoulEssence(0);
        gemItem = 0;
        updateGemItem(0);
        boxesDestroyed = 0;
        updateBoxesDestroyed(0);
        revives = 3;
        updateRevives(0);

        defaultSoulEssence = 0;
    }


    public void pretendTally()
    {
        defaultSoulEssence = 0f;
        defaultBoxDestroyed = 0f;
        defaultGemItem = 0f;
        UpdateStatsText();

        StartCoroutine(AddOverTime());
    }

    private void UpdateStatsText()
    {
        // if(defaultSoulEssence >= (soulEssence * revives))
        // {
        //     pulseGameObject pulseSoulStat = bankedSoulEssenceText.gameObject.transform.parent.GetComponent<pulseGameObject>();
        //     pulseSoulStat.Pulse();
        // }

        // if(defaultBoxDestroyed >= boxesDestroyed)
        // {
        //     pulseGameObject pulseSoulStat = bankedBoxDestroyedText.gameObject.transform.parent.GetComponent<pulseGameObject>();
        //     pulseSoulStat.Pulse();
        // }

        // if(defaultGemItem >= gemItem)
        // {
        //     pulseGameObject pulseSoulStat = bankedGemItemsText.gameObject.transform.parent.GetComponent<pulseGameObject>();
        //     pulseSoulStat.Pulse();
        // }

        bankedSoulEssenceText.text = defaultSoulEssence.ToString();
        bankedBoxDestroyedText.text = defaultBoxDestroyed.ToString();
        bankedGemItemsText.text = defaultGemItem.ToString();
    }

    private IEnumerator AddOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.025f);
            if (defaultSoulEssence < (soulEssence * revives))
            {
                defaultSoulEssence += 1f;
                UpdateStatsText();

                if(defaultSoulEssence + 1f == (soulEssence * revives))
                {
                    pulseGameObject pulseSoulStat = bankedSoulEssenceText.gameObject.transform.parent.GetComponent<pulseGameObject>();
                    pulseSoulStat.Pulse();
                }
            }

            else if (defaultBoxDestroyed < boxesDestroyed)
            {
                defaultBoxDestroyed += 1f;
                UpdateStatsText();

                if(defaultBoxDestroyed + 1f == boxesDestroyed)
                {
                    pulseGameObject pulseSoulStat = bankedBoxDestroyedText.gameObject.transform.parent.GetComponent<pulseGameObject>();
                    pulseSoulStat.Pulse();
                }
            }

            else if (defaultGemItem < gemItem)
            {
                defaultGemItem += 1f;
                UpdateStatsText();

                if(defaultGemItem + 1f == gemItem)
                {
                    pulseGameObject pulseSoulStat = bankedGemItemsText.gameObject.transform.parent.GetComponent<pulseGameObject>();
                    pulseSoulStat.Pulse();
                }
            }
        }
    }

    // public void pretendTally()
    // {
    //     bankedSoulEssenceText.text = defaultSoulEssence.ToString();

    //     if(defaultSoulEssence < ((soulEssence) * revives))
    //     {
    //         defaultSoulEssence += 0.001f;
    //         bankedSoulEssenceText.text = Mathf.Round(Mathf.Floor(defaultSoulEssence)).ToString();
    //         pretendTally();
    //     }
    // }
}
