using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteLevelManager : MonoBehaviour
{
    public GameObject planePrefab;      // Prefab for the ground plane
    public Transform player;
    
    public Vector3 checkBoxSize = new Vector3(1.0f, 1.0f, 1.0f); // Size of the checkbox volume
    public float checkDist = 10f;
    public List<Transform> checkBoxCenter = new List<Transform>(); // Transform representing the center of the checkbox volume
    public LayerMask collisionLayers; // Layers to consider for collision

    private void Start()
    {
        player = GameObject.Find("Ghost Player").transform;
        for(int i = 0; i < 8; i++)
        {
            checkBoxCenter.Add(transform.GetChild(i).transform);
        }
    } 

    private void Update()
    {
        float dist;
        for(int i = 0; i < checkBoxCenter.Count; i++)
        {
            bool collisionDetected = Physics.CheckBox(checkBoxCenter[i].position, checkBoxSize/2, Quaternion.identity, collisionLayers);

            if(!collisionDetected)
            {           
                dist = Vector3.Distance(player.position, checkBoxCenter[i].position);                
                if(dist < checkDist)
                {
                    GameObject ground = Instantiate(planePrefab, checkBoxCenter[i].position, Quaternion.identity, transform.parent.transform);
                }
            }
        }
        
        dist = Vector3.Distance(player.position, transform.position); 
        if(dist > checkDist)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for(int i = 0; i < checkBoxCenter.Count; i++)
        {
            Gizmos.DrawWireCube(checkBoxCenter[i].position, checkBoxSize);
        }
    }
}
