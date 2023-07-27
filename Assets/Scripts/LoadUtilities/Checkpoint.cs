using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lowscope.Saving;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(!PavelPlayerSettingStates.current.isDead)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Checkpoint reached");
                SaveMaster.WriteActiveSaveToDisk();
                
            }
        }
        
    }
}
