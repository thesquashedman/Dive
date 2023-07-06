using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorLighting : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject globalLight;
    void Awake() {
        
        //Stops the shadow layer from being rendered in the editor, so that we can see the scene better
        //Shouldn't really be set here, since it's a permanent thing, but I have no clue how else to do it.
        //Tools.visibleLayers = ~((1 << LayerMask.NameToLayer("Shadow")));
        //Turn off the global light when the scene starts. Global light is so that there is no darkness in the editor
        globalLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
