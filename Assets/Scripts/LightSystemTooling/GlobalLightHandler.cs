using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLightHandler : MonoBehaviour
{
    public static GlobalLightHandler current;
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
        
    }
}
