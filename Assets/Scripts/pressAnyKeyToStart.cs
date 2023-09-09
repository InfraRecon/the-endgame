using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressAnyKeyToStart : MonoBehaviour
{
    public AudioSource sourceBump;
    public AudioClip clipSelect;
    public AudioClip clipActivate;

    public GameObject startScreenCamera;
    public GameObject mainScreenCamera;
    public GameObject mainMenuCamera;

    public GameObject startScreen;
    public bool startScreenActive = true;

    public GameObject menuScreen;
    public bool menuScreenActive = false;

    public GameObject levelSelectScreen;
    public GameObject levelSelectScreenIcon;
    public GameObject loadingScreen;
    public GameObject deathScreen;
    public GameObject statScreen;

    public ThirdPersonCameraMovement thirdPersonCameraMovement;
    public ThirdPersonCameraAttack thirdPersonCameraAttack; 

    void Start()
    {
        thirdPersonCameraMovement.enabled = false;
        thirdPersonCameraAttack.enabled = false;
    }

    void Update()
    {
        if(startScreenActive)
        {
            if (Input.anyKey)
            {
                toggleStartScreen(false);
                startScreenActive = false;
            }
        }
        else if(startScreen.activeSelf)
        {
                toggleStartScreen(true);
                startScreenActive = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape) && 
        !levelSelectScreen.activeSelf && 
        !deathScreen.activeSelf &&
        !statScreen.activeSelf &&
        !menuScreenActive ||
        Input.GetKeyDown(KeyCode.JoystickButton9) && 
        !levelSelectScreen.activeSelf && 
        !deathScreen.activeSelf &&
        !statScreen.activeSelf &&
        !menuScreenActive)
        {
            toggleMenuScreen(true);
            menuScreenActive = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && 
        !levelSelectScreen.activeSelf && 
        !deathScreen.activeSelf &&
        !statScreen.activeSelf &&
        menuScreenActive ||
        Input.GetKeyDown(KeyCode.JoystickButton9) && 
        !levelSelectScreen.activeSelf && 
        !deathScreen.activeSelf &&
        !statScreen.activeSelf &&
        menuScreenActive)
        {
            toggleMenuScreen(false);
            menuScreenActive = false;
        }

        if(!levelSelectScreen.activeSelf)
        {
            levelSelectScreenIcon.SetActive(true);
        }
        else
        {
            levelSelectScreenIcon.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow) &&
           menuScreen.activeSelf ||
           Input.GetKeyDown(KeyCode.RightArrow) &&
           menuScreen.activeSelf ||
           Input.GetKeyDown(KeyCode.UpArrow) &&
           menuScreen.activeSelf ||
           Input.GetKeyDown(KeyCode.DownArrow) &&
           menuScreen.activeSelf)
        {
            sourceBump.clip = clipSelect;
            sourceBump.pitch = Random.Range(0.8f, 1f);
            sourceBump.Play(); 
        }
        else if(Input.GetKeyDown(KeyCode.Return) &&
                menuScreen.activeSelf)
        {
            sourceBump.clip = clipActivate;
            sourceBump.pitch = Random.Range(0.8f, 1f);
            sourceBump.Play(); 
        }
    }

    public void toggleStartScreen(bool startScreenIsActive)
    {
        if(!startScreenIsActive)
        {   
            thirdPersonCameraMovement.enabled = true;
            thirdPersonCameraAttack.enabled = true;
            startScreenCamera.SetActive(false);
            mainScreenCamera.SetActive(true);
            startScreen.SetActive(false);
        }
        else if(startScreenIsActive)
        {   
            thirdPersonCameraMovement.enabled = false;
            thirdPersonCameraAttack.enabled = false;
            startScreenCamera.SetActive(true);
            mainScreenCamera.SetActive(false);
            startScreen.SetActive(true);
            menuScreen.SetActive(false);
            levelSelectScreen.SetActive(false);
            loadingScreen.SetActive(false);
            deathScreen.SetActive(false);
            statScreen.SetActive(false);
        }
    }
    public void toggleMenuScreen(bool toggle)
    {
        if(toggle)
        {
            thirdPersonCameraMovement.enabled = false;
            thirdPersonCameraAttack.enabled = false;
            startScreenCamera.SetActive(false);
            mainMenuCamera.SetActive(true);
            menuScreen.SetActive(true);
            startScreen.SetActive(false);
        }
        else if(!toggle)
        {
            thirdPersonCameraMovement.enabled = true;
            thirdPersonCameraAttack.enabled = true;
            mainMenuCamera.SetActive(false);
            menuScreen.SetActive(false);
        }
    }
}
