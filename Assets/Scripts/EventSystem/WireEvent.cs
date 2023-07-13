using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireEvent : MonoBehaviour
{
    //Name of the taskCompleted to be triggered by
    public string eventName = "Wirefixing";

    //Sound in AudioManager to play on task
    public string playName = "Explosion";

    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onTaskCompleted += onCompletion;
    }

    private void onCompletion(string id) {
        if(id == eventName) {
            AudioManager.instance.Play(playName);
        }
    }

    private void onDisable() {
        EventManager.current.onTaskCompleted -= onCompletion;
    }
}
