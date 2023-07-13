using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public Image loadingBar;
    void Start()
    {
        StartCoroutine(LoadAsync());
    }

    // Update is called once per frame
    IEnumerator LoadAsync()
    {
        float progress = 0f;
        AsyncOperation operation = SceneManager.LoadSceneAsync(LoadingData.sceneToLoad);
        operation.allowSceneActivation = false;
        while(progress < 1f)
        {
            Debug.Log(operation.progress);
            loadingBar.fillAmount = progress;
            progress += .005f;
            yield return new WaitForSeconds(.01f);
        }
        while (!operation.isDone && progress >= 1f) {
 
            operation.allowSceneActivation = true; //here the scene is definitely loaded.
 
            yield return null;
        }
    }
 
}
        
        
    