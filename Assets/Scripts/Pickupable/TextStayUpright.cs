using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextStayUpright : MonoBehaviour
{

    //https://answers.unity.com/questions/479943/how-to-keep-text-objects-upright.html
    public float offset;
    void Start() {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.position +  new Vector3(0, offset, 0);
        transform.rotation = Camera.main.transform.rotation;
    }
}
