using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightHandler : MonoBehaviour
{
    struct SpriteAndColor
    {
        public SpriteRenderer renderer;
        public Color startingColor;
    }
    public static GlobalLightHandler current;
    public RenderTexture globalLighting;
    public Light2D globalLight; 
    List<SpriteAndColor> spriteRenderers = new List<SpriteAndColor>();

    public float lightMax = 1;
    public float lightMin = 0;

    public float colorDarkenMaxPercentage = 0.5f;

    public float lerpspeed = 0.05f;
    public float colorLerpSpeed = 0.25f;

    public Texture2D currentTexture;
    private void Awake() {
        //Singleton pattern
        if(current != null && current != this) {
            Destroy(this);
        } else {
            current = this;
        }
    }
    private void Start() {
        GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("Background");
        foreach(GameObject background in backgrounds)
        {
            SpriteAndColor temp = new SpriteAndColor();
            
            temp.renderer = background.GetComponent<SpriteRenderer>();
            temp.startingColor = background.GetComponent<SpriteRenderer>().color;
            spriteRenderers.Add(temp);
        }
        //spriteRenderers = GetComponents<SpriteRenderer>();

    }
    

    // Update is called once per frame
    void Update()
    {
        
        //float previousIntensity = globalLight.intensity;
        float intensity = GetIntensity(globalLighting);
        float lerp = Mathf.Lerp((globalLight.intensity - lightMin) / (lightMax -lightMin), intensity, lerpspeed);
        globalLight.intensity = lerp * (lightMax - lightMin) + lightMin;
        
        foreach (SpriteAndColor sprite in spriteRenderers)
        {
            //docs.unity3d.com/ScriptReference/Color.RGBToHSV.html
            //docs.unity3d.com/ScriptReference/Color.HSVToRGB.html
            float H, S, V;
            Color.RGBToHSV(sprite.startingColor.linear, out H, out S, out V);
            V *= 1 - (colorDarkenMaxPercentage * 1 - intensity);
            Color newColor = Color.Lerp(sprite.renderer.color, Color.HSVToRGB(H, S, V).gamma, colorLerpSpeed);
            newColor.a = sprite.startingColor.a;
            sprite.renderer.color = newColor;
            /*
            Color newColor = (sprite.startingColor * (1 - darkenPercent * (1 - lerp))).gamma;
            newColor.a = sprite.startingColor.a;
            sprite.renderer.color = newColor;
            */
        }
    }
    public float GetIntensity(RenderTexture rt)
    {
        // Remember currently active render texture
        RenderTexture currentActiveRT = RenderTexture.active;

        // Set the supplied RenderTexture as the active one
        RenderTexture.active = rt;

        // Create a new Texture2D and read the RenderTexture image into it
        if (currentTexture == null)
            currentTexture = new Texture2D(1, 1, TextureFormat.RGBA32, false, true);
 
        // copy the single pixel value from the render texture to the texture2D on the GPU
        currentTexture.ReadPixels(new Rect(rt.width / 2,rt.height / 2,1,1), 0, 0, false);
        //Texture2D tex = new Texture2D(rt.width, rt.height);
        //tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        
        
        // Restorie previously active render texture
        Debug.Log("Texture" + currentTexture.GetPixel(0, 0));
        RenderTexture.active = currentActiveRT;
        Debug.Log("intensity" + currentTexture.GetPixel(0, 0).grayscale);
        return currentTexture.GetPixel(0, 0).grayscale;
    }
}
