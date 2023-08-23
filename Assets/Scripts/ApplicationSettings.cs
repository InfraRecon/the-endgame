using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ApplicationSettings : MonoBehaviour
{
    public int setFramerate = 30;
    public TextMeshProUGUI frameRateText;
    void Start()
    {
        // Make the game run as fast as possible
        Application.targetFrameRate = setFramerate;
    }

    public void Update()
    {
        frameRateText.text = Mathf.Round(1.0f / Time.deltaTime) + " fps";
    }

    public void bruteForceFramerate(int fps)
    {
        Application.targetFrameRate = fps;
    }
}