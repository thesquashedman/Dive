using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadingData : MonoBehaviour
{
    public static string sceneToLoad = "";
    private void Awake() {
        DontDestroyOnLoad(this);
    }
    public static void LoadSceneName()
    {
        if (File.Exists(Application.persistentDataPath + "/CurrentScene"))
        {
            using (var reader = new BinaryReader(File.Open(Application.persistentDataPath + "/CurrentScene", FileMode.Open)))
            {
                sceneToLoad = reader.ReadString();
            }
        }
    }
    void OnDestroy() {
        using (var writer = new BinaryWriter(File.Open(Application.persistentDataPath + "/CurrentScene", FileMode.Create)))
        {

            writer.Write(sceneToLoad);
        }
    }
    
    
}
