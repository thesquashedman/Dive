using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SceneCulling : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake() {
        
        Tools.visibleLayers = ~((1 << LayerMask.NameToLayer("Shadow")));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
