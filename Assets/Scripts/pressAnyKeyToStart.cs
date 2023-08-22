using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressAnyKeyToStart : MonoBehaviour
{
    public GameObject startScreenCamera;
    public GameObject mainScreenCamera;
    public GameObject mainMenuCamera;

    public GameObject startScreen;
    public bool startScreenActive = true;

    public GameObject menuScreen;
    public bool menuScreenActive = false;

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
                toggleStartScreen();
                startScreenActive = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(!menuScreenActive)
            {
                toggleMenuScreen(true);
                menuScreenActive = true;
            }
            else if(menuScreenActive)
            {
                toggleMenuScreen(false);
                menuScreenActive = false;
            }
        }
    }
    public void toggleStartScreen()
    {
        thirdPersonCameraMovement.enabled = true;
        thirdPersonCameraAttack.enabled = true;
        startScreenCamera.SetActive(false);
        mainScreenCamera.SetActive(true);
        startScreen.SetActive(false);
    }
    public void toggleMenuScreen(bool toggle)
    {
        thirdPersonCameraMovement.enabled = !toggle;
        thirdPersonCameraAttack.enabled = !toggle;
        startScreenCamera.SetActive(!toggle);
        mainScreenCamera.SetActive(!toggle);
        mainMenuCamera.SetActive(toggle);
        menuScreen.SetActive(toggle);
    }
}
