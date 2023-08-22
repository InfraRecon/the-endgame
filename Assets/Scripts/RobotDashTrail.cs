using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDashTrail : MonoBehaviour
{
    public float activeTime = 2f;

    [Header("Mesh Related")]
    public float meshRefreshRate = 0.1f;
    public float meshDestroyDelay = 3f;
    public Transform postionToSpawn;

    [Header("Shader Related")]
    public Material mat;

    private bool isTrailActive;
    private SkinnedMeshRenderer[] skinnedMeshRenderers;
    /////////
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.X) && !isTrailActive)
        {
            isTrailActive = true;
            StartCoroutine(ActivateTrail(activeTime));
        }
    }

    IEnumerator ActivateTrail (float timeActive)
    {
        // while (timeActive > 0)
        // {
        //     timeActive -= meshRefreshRate;
            Color newColor = new Color(0.3f, 0.4f, 0.6f, 1f);
            if(skinnedMeshRenderers == null)    
            {
                skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            }

            for(int i=0; i < skinnedMeshRenderers.Length; i++)
            {
                GameObject gObj = new GameObject();
                // rotationToSpawn = GameObject.Find("mixamorig:Spine").GetComponent<Transform>();
                //new Quaternion(rotationToSpawn.rotation.x,rotationToSpawn.rotation.y, rotationToSpawn.rotation.z, 1)
                gObj.transform.SetPositionAndRotation(postionToSpawn.position, postionToSpawn.rotation);
                MeshRenderer mr = gObj.AddComponent<MeshRenderer>();
                MeshFilter mf = gObj.AddComponent<MeshFilter>();
                //gameObjectScaleReducer sc = gObj.AddComponent(typeof(gameObjectScaleReducer)) as gameObjectScaleReducer;

                Mesh mesh = new Mesh();
                skinnedMeshRenderers[i].BakeMesh(mesh);

                mf.mesh = mesh;
                mr.material = mat;

                // mr.material.SetColor("Fader_Shader_Color", new Color(newColor.r, newColor.g, newColor.b, newColor.a - 0.5f));

                Destroy(gObj, meshDestroyDelay);
            }

            yield return new WaitForSeconds(meshRefreshRate);
        // }

        isTrailActive = false;
    }
}
