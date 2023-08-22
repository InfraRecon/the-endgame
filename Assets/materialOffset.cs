using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialOffset : MonoBehaviour
{
    public Material material;       // Reference to the material
    public float speed = 1f;         // Speed of the tiling movement
    public float scale = 1f;         // Scale of the Perlin noise
    public string textureName = "_MainTex";  // Name of the texture property in the shader
    private Vector2 offset = Vector2.zero;  // Current offset for the tiling

    void Start()
    {
        // Renderer renderer = GetComponent<Renderer>();
        // if (renderer != null)
        // {
        //     material = renderer.material;
        // }
        // else
        // {
        //     Debug.LogError("No Renderer component found on the object!");
        // }
    }

    void Update()
    {
        // Update the offset based on Perlin noise
        offset.x += Time.deltaTime * speed;
        offset.y += Time.deltaTime * speed;
        Vector2 noiseOffset =   new Vector2(1 - Mathf.PerlinNoise(offset.x, offset.x) * scale, 1 - Mathf.PerlinNoise(offset.y, offset.y) * scale);
        //Vector2 noiseOffset = new Vector2(offset.x * scale, offset.y * scale);

        // Set the offset in the material
        if (material != null)
        {
            material.SetTextureScale(textureName, noiseOffset);
        }
    }
}
