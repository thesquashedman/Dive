using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public string[] bgNames;
    
    // Start is called before the first frame update
    void Start()
    {
        //Put events here
        AudioManager.instance.Play(bgNames[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void playBG(int num) {
        if(num < 0 || num > bgNames.length) {
            Debug.Log("playBG: Index Out of Bounds!");
            return;
        }
        
        AudioManager.instance.Play(bgNames[num]);
    }

    private void OnDisable() {
        //Remove events here
    }
}
