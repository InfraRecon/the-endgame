using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    public Transform player;
    private ThirdPersonCameraMovement thirdPersonCameraMovement;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Ghost Player").GetComponent<Transform>();
        thirdPersonCameraMovement = player.GetComponent<ThirdPersonCameraMovement>();
    }

    public void restePlayerToHub()
    {
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1f);
        player.transform.position = transform.position;
        thirdPersonCameraMovement.enabled = true;
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
