using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDisplayRender : MonoBehaviour
{
    public int newWidth = 1024; // New width for the Render Texture
    public int newHeight = 768; // New height for the Render Texture
    public RenderTexture originalTexture;
    public RenderTexture targetRenderTexture; // Reference to the Render Texture you want to modify
    public Camera gameDisplayCamera;
    public Material displayMaterial;

    private void Start()
    {
        // Make sure a Render Texture is assigned
        if (targetRenderTexture == null)
        {
            Debug.LogError("Target Render Texture is not assigned!");
            return;
        }

        // Update the Render Texture resolution
        UpdateRenderTextureResolutionToSD();
    }

    public void UpdateRenderTextureResolutionToHD()
    {
        newWidth = 1920; // New width for the Render Texture
        newHeight = 1080; // New height for the Render Texture
        UpdateRenderTextureResolution();
    }

    public void UpdateRenderTextureResolutionToSD()
    {
        newWidth = 1280; // New width for the Render Texture
        newHeight = 720; // New height for the Render Texture
        UpdateRenderTextureResolution();
    }

    public void UpdateRenderTextureResolutionTo8bit()
    {
        newWidth = 57*5; // New width for the Render Texture
        newHeight = 32*5; // New height for the Render Texture
        UpdateRenderTextureResolution();
    }

    private void UpdateRenderTextureResolution()
    {
        // Create a new Render Texture with the desired resolution
        RenderTexture newRenderTexture = new RenderTexture(newWidth, newHeight, targetRenderTexture.depth, targetRenderTexture.format);
        newRenderTexture.antiAliasing = targetRenderTexture.antiAliasing;
        newRenderTexture.anisoLevel = targetRenderTexture.anisoLevel;
        newRenderTexture.filterMode = targetRenderTexture.filterMode;
        newRenderTexture.wrapMode = targetRenderTexture.wrapMode;

        // Copy the contents of the original Render Texture to the new one
        Graphics.Blit(targetRenderTexture, newRenderTexture);

        // Release the original Render Texture
        targetRenderTexture.Release();

        // Assign the new Render Texture to the target
        targetRenderTexture = newRenderTexture;

        // Apply the new Render Texture to a material or camera
        // For example: GetComponent<Renderer>().material.mainTexture = targetRenderTexture;
        displayMaterial.mainTexture = targetRenderTexture;

        gameDisplayCamera.targetTexture = targetRenderTexture;
    }
}
