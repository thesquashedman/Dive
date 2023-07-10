using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    public int gameStartScene;

    public void StartGame() {
        SceneManager.LoadScene(gameStartScene);
    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
