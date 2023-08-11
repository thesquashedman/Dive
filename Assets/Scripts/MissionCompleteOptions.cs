using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionCompleteOptions : MonoBehaviour
{
    // Start is called before the first frame update
    public string nextLevelName;
    public void NextLevel()
    {
        
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(nextLevelName);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
