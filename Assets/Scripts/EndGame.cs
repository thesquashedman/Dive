using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject winMessage;
    
    bool isDone = false;
    void Start()
    {
        EventManager.current.onLevelWin += Win;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Win()
    {
        isDone = true;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            if(isDone)
            {
                Debug.Log("End!");
                winMessage.SetActive(true);
                PavelPlayerSettingStates.current.GetComponent<PavelPlayerHealth>().enabled = false;
                EventManager.current.PlayerBeatLevel();
            }
        }
    }
}
