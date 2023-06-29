using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    //Flip the scale of the object when the players rotation is changed
    // Start is called before the first frame update
    public float lowerAngle = 90;
    public float upperAngle = 270;

    public bool isFlipped = false;
    
    void Start()
    {
        if (transform.eulerAngles.z > lowerAngle && transform.eulerAngles.z < upperAngle)
        {
            isFlipped = true;
        }
        else
        {
            isFlipped = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
        //Write me a script to flip the scale of the object when the players rotation is changed
        if (transform.eulerAngles.z > lowerAngle && transform.eulerAngles.z < upperAngle)
        {
            if (!isFlipped)
            {
                transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
                isFlipped = true;
            }
        }
        else
        {
            if (isFlipped)
            {
                transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
                isFlipped = false;
            }
        }
        
    }
}
