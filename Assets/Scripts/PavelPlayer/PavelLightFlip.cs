using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavelLightFlip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onPlayerFlip += FlipRotation;
    }
    private void OnDisable() {
        EventManager.current.onPlayerFlip -= FlipRotation;
    }

    // Update is called once per frame
    void FlipRotation(bool flip)
    {
        if(flip)
        {
            transform.localEulerAngles = new Vector3(0, 0, 180);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
    }
    void Update()
    {
        
    }
}
