using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Written by Justin

Script meant to activate a loading box that will
lead the player to another level once a win
condition has been met.
*/
public class LoadBox : MonoBehaviour
{
    private bool isActive = false;

    public string name = "";

    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onLevelWin += activate;
    }

    private void activate() {
        isActive = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(isActive) {
            SceneManager.LoadSceneAsync(name);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }
    }

    void OnDisable() {
        EventManager.current.onLevelWin -= activate;
    }

    
}
