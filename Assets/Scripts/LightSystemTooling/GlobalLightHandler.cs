using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightHandler : MonoBehaviour
{
    public static GlobalLightHandler current;
    public RenderTexture globalLighting;
    public Light2D globalLight; 

    public float lerpspeed = 0.05f;
    private void Awake() {
        //Singleton pattern
        if(current != null && current != this) {
            Destroy(this);
        } else {
            current = this;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        Texture2D temp = GetRTPixels(globalLighting);
        //float previousIntensity = globalLight.intensity;
        float intensity = temp.GetPixel(globalLighting.width/2, globalLighting.height/2).grayscale;
        globalLight.intensity = Mathf.Lerp(globalLight.intensity, intensity, lerpspeed);
        Destroy(temp);
    }
    static public Texture2D GetRTPixels(RenderTexture rt)
    {
        // Remember currently active render texture
        RenderTexture currentActiveRT = RenderTexture.active;

        // Set the supplied RenderTexture as the active one
        RenderTexture.active = rt;

        // Create a new Texture2D and read the RenderTexture image into it
        Texture2D tex = new Texture2D(rt.width, rt.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

        // Restorie previously active render texture
        RenderTexture.active = currentActiveRT;
        return tex;
    }
}
