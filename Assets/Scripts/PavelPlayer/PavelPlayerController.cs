using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavelPlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            EventManager.current.PlayerStartMove();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            EventManager.current.PlayerStopMove();
        }
        if(Input.GetKey(KeyCode.X))
        {
            EventManager.current.PlayerAttack();
        }
        
    }
}
