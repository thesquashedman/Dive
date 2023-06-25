using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionRenderTexture : MonoBehaviour
{
    public RenderTexture renderTexture;

    public Camera camera;

    int lastScreenWidth = 0;
    int lastScreenHeight = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lastScreenWidth != Screen.width || lastScreenHeight != Screen.height)
        {
            Debug.Log("Screen size changed");
            renderTexture.Release();
            renderTexture.width = Screen.width;
            renderTexture.height = Screen.height;
            renderTexture.Create();
            camera.aspect = (float)Screen.width / (float)Screen.height;

            //renderTexture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.Default); //<--- create this function
            //renderTexture.Create();
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
           
        }
    }
}
