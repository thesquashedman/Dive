using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesCompleteFromStart : MonoBehaviour
{
    // Start is called before the first frame update
    bool won = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!won)
        {
            EventManager.current.levelWin();
            won = true;
        }
        
    }
}
