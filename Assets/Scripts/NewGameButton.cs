using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Lowscope.Saving;

public class NewGameButton : MonoBehaviour
{
    public string sceneName;
    public Button continueButton;

    void Awake() {
        LoadingData.LoadSceneName();
        if(LoadingData.sceneToLoad == "Default") {
            continueButton.interactable = false;
        } else {
            continueButton.interactable = true;
        }
    }
    public void ContinueGame() {
        //LoadingData.LoadSceneName();
        SceneManager.LoadScene("LoadingScreen");
    }
    public void StartNewGame() {
        //Should first give prompt so the player doesn't accidentally delete their save
        SaveMaster.DeleteSave();
        LoadingData.sceneToLoad = sceneName;
        SceneManager.LoadScene("LoadingScreen");
    }
    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
