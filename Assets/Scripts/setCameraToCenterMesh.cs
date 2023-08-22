using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCameraToCenterMesh : MonoBehaviour
{
    public MeshFilter meshFilter;
    private Transform objectToTrack;

    private Transform objectToUpdate;
    
    private Vector3[] meshVertices;
    private float meshMinX;
    private float meshMaxX;


    ///

    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    
    void Start()
    {
        objectToTrack = GameObject.Find("Ghost Player").transform;
        objectToUpdate = GameObject.Find("Camera").transform;
    }
    void Update()
    {
        // Get the mesh vertices from the MeshFilter component
        meshVertices = meshFilter.mesh.vertices;

        // Set the initial min and max x positions to the first vertex's x value
        meshMinX = meshVertices[0].x;
        meshMaxX = meshVertices[0].x;
        
        // Find the min and max x positions of the mesh vertices
        for (int i = 1; i < meshVertices.Length; i++)
        {
            float x = meshVertices[i].x;
            if (x < meshMinX) {
                meshMinX = x;
            }
            else if (x > meshMaxX) {
                meshMaxX = x;
            }
        }

        // Get the position of the objectToTrack
        Vector3 objectPos = objectToTrack.position;
        
        // Calculate the center point of the mesh based on the object's x position
        float meshCenterX = Mathf.Lerp(meshMinX, meshMaxX, (objectPos.x - meshMinX) / (meshMaxX - meshMinX));
        
        if(objectToUpdate.localPosition.x + meshCenterX <= meshMaxX &&
        objectToUpdate.localPosition.x + meshCenterX >= meshMinX)
        {
            //objectToUpdate.localPosition = new Vector3(objectToUpdate.localPosition.x + meshCenterX, objectToUpdate.localPosition.y, objectToUpdate.localPosition.z);

            // Smoothly move the camera towards that target position
            objectToUpdate.localPosition = Vector3.SmoothDamp(objectToUpdate.localPosition, new Vector3(objectToUpdate.localPosition.x + meshCenterX, objectToUpdate.localPosition.y, objectToUpdate.localPosition.z), ref velocity, smoothTime);

        }
        // Output the center point x value
        Debug.Log("Mesh center point X: " + meshCenterX);
    }
}
