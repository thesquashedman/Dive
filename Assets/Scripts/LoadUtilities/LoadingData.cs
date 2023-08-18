using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadingData : MonoBehaviour
{
    public static string sceneToLoad = "";
    public static LoadingData current;

    private void Awake() {
        if (current != null && current != this)
        {
            Destroy(this);
        }
        else
        {
            current = this;
        }
        DontDestroyOnLoad(this);
        LoadingData.LoadSceneName();
    }
    public static void LoadSceneName()
    {
        if (sceneToLoad == "")
        {
            if (File.Exists(Application.persistentDataPath + "/CurrentScene"))
            {
                using (var reader = new BinaryReader(File.Open(Application.persistentDataPath + "/CurrentScene", FileMode.Open)))
                {
                    sceneToLoad = reader.ReadString();
                }
            }
        }
    }
    void OnDestroy() {
        using (var writer = new BinaryWriter(File.Open(Application.persistentDataPath + "/CurrentScene", FileMode.Create)))
        {
            if(sceneToLoad != "MainMenu")
            {
                writer.Write(sceneToLoad);
            }
            
        }
    }
    
    
}
