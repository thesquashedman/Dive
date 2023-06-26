using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Post : MonoBehaviour
{
    // Start is called before the first frame update
    public Material mat;
    public Material mat2;
    RenderTexture temp;
    
    void Start()
    {
        temp = new RenderTexture(Camera.main.pixelWidth, Camera.main.pixelHeight, 16);
        //GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnRenderImage(RenderTexture src, RenderTexture dest) {
        
        /*
        Camera camera = Camera.main;
        Vector4 pos = new Vector4(0, 0, 0, 1);
        Matrix4x4 m = (camera.projectionMatrix * camera.worldToCameraMatrix);
        pos = camera.worldToCameraMatrix * pos;
        Debug.Log(pos);
        mat.SetMatrix("mMatrix", m);
        //Debug.Log(manualWorldToScreenPoint(pos));
        Graphics.Blit(src, dest, mat);
        */
        //var camera = GetComponent<Camera>();
        //mat.SetMatrix("_ViewProjectInverse", (camera.projectionMatrix * camera.worldToCameraMatrix).inverse);
        
        //Graphics.Blit(src, dest, mat);
        Graphics.Blit(src, temp, mat2);
        Graphics.Blit(temp, dest, mat);
    }
    /*
    Vector3 manualWorldToScreenPoint(Vector3 wp) {
        // calculate view-projection matrix
        Matrix4x4 mat = Camera.main.projectionMatrix * Camera.main.worldToCameraMatrix;

        // multiply world point by VP matrix
        Vector4 temp = mat * new Vector4(wp.x, wp.y, wp.z, 1f);

        if (temp.w == 0f) {
            // point is exactly on camera focus point, screen point is undefined
            // unity handles this by returning 0,0,0
            return Vector3.zero;
        } else {
            // convert x and y from clip space to window coordinates
            temp.x = ((temp.x/temp.w + 1f)*.5f * Camera.main.pixelWidth) / Camera.main.pixelWidth;
            temp.y = ((temp.y/temp.w + 1f)*.5f * Camera.main.pixelHeight) / Camera.main.pixelHeight;
            return new Vector3(temp.x, temp.y, wp.z);
        }
    }
    */
}
