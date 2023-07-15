using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipX : MonoBehaviour
{
    //Flip the scale of the object when the players rotation is changed
    // Start is called before the first frame update
    public float lowerAngle = 90;
    public float upperAngle = 270;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lowerAngle > upperAngle)
        {
            if (transform.eulerAngles.z > lowerAngle || transform.eulerAngles.z < upperAngle)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            if (transform.eulerAngles.z > lowerAngle && transform.eulerAngles.z < upperAngle)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);
        }
        Debug.Log(transform.eulerAngles.z);
        
    }
}

