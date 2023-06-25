using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
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
        
        //Debug.Log(transform.eulerAngles);
        if (transform.eulerAngles.z > lowerAngle && transform.eulerAngles.z < upperAngle)
            transform.localScale = new Vector3(1, -1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }
}
