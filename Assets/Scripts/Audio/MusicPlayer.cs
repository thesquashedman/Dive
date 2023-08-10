using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public string[] bgNames;
    
    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "Level Two (Mikhail)") {
            playBG(1);
        } else {
            //Put events here
            playBG(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void playBG(int num) {
        if(num < 0 || num > bgNames.Length) {
            Debug.Log("playBG: Index Out of Bounds!");
            return;
        }
        
        AudioManager.instance.Play(bgNames[num]);
    }

    private void OnDisable() {
        //Remove events here
    }
}
