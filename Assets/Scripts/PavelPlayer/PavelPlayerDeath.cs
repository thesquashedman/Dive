using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Lowscope.Saving;
using Lowscope.Tools;

public class PavelPlayerDeath : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject deathMessage;
    void Awake()
    {
        
    }
    void Start()
    {
        EventManager.current.onPlayerDeath += Death;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Death()
    {
        //deathMessage.SetActive(true);
        this.GetComponent<PavelMovement>().enabled = false;
        this.GetComponent<Rigidbody2D>().gravityScale = 1;
        StartCoroutine(Reload());
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(5);
        LoadingData.sceneToLoad = SceneManager.GetActiveScene().name;
        
        SceneManager.LoadScene("LoadingScreen");
        //SceneManager.UnloadScene(SceneManager.GetActiveScene().name);
    }
}
