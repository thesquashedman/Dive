using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipLight : MonoBehaviour
{
    // Start is called before the first frame update
    public Flip flip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(flip.isFlipped)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, 180);
        }
    }
}
