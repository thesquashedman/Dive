using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureFlip : MonoBehaviour
{
    // The angles at which the texture should be flipped.
    public float lowerAngle = 90f;
    public float upperAngle = 270f;

    // Reference to the line renderer that contains the texture.
    public LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lowerAngle > upperAngle)
        {
            if (transform.eulerAngles.z > lowerAngle || transform.eulerAngles.z < upperAngle)
            {
                lineRenderer.material.mainTextureScale = new Vector2(1, -1);
                lineRenderer.material.mainTextureOffset = new Vector2(0, 1);
            }
            else
            {
                lineRenderer.material.mainTextureScale = new Vector2(1, 1);
                lineRenderer.material.mainTextureOffset = new Vector2(0, 0);
            }
        }
        else
        {
            if (transform.eulerAngles.z > lowerAngle && transform.eulerAngles.z < upperAngle)
            {
                lineRenderer.material.mainTextureScale = new Vector2(1, -1);
                lineRenderer.material.mainTextureOffset = new Vector2(0, 1);
            }
            else
            {
                lineRenderer.material.mainTextureScale = new Vector2(1, 1);
                lineRenderer.material.mainTextureOffset = new Vector2(0, 0);
            }
        }
    }
}
