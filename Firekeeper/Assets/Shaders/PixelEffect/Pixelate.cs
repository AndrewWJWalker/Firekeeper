using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class Pixelate : MonoBehaviour
{

    public Material imageEffectMaterial;
    public int targetWidth = 500;
    public CameraEvent camEvent;

    Camera cam;
    RenderTexture renderTexture;

    void Start()
    {



        cam = Camera.main;
        CommandBuffer commandBuffer = new CommandBuffer();
        commandBuffer.name = "Pixelate";

        int downsample =(int)(cam.pixelWidth / targetWidth);
        //check if modulus of downsample rate is a multiple of two, if not, add 1
        if (downsample % 2 != 0)
        {
            downsample++;
        }
        if (downsample <= 2)
        {
            downsample = 4;
        }
        Debug.Log("Downsample rate: " + downsample);
        
         renderTexture = new RenderTexture((int)(cam.pixelWidth / downsample), (int)(cam.pixelHeight / downsample), 0);
        renderTexture.filterMode = FilterMode.Point;
        RenderTargetIdentifier rtID = new RenderTargetIdentifier(renderTexture);

        RenderTexture screenCopy = screenCopy = new RenderTexture(cam.pixelWidth, cam.pixelHeight, 0);
        screenCopy.filterMode = FilterMode.Point;
        
        commandBuffer.SetRenderTarget(rtID);
        commandBuffer.ClearRenderTarget(true, true, new Color(0, 0, 0, 0), 1f);

        
 
        commandBuffer.Blit(BuiltinRenderTextureType.CameraTarget, screenCopy);
        commandBuffer.Blit(screenCopy, rtID, imageEffectMaterial);

        cam.AddCommandBuffer(camEvent, commandBuffer);
    }

    void Update()
    {
        
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(renderTexture, destination);
    }
}
