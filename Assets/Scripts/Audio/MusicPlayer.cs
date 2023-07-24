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

    private void playBG() {
        AudioManager.instance.Play(bgNames[1]);
    }

    private void OnDisable() {
        //Remove events here
    }
}
