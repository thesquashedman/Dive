using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PavelPlayerTalk : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI playerText;
    public float textSpeed = 1;
    int increment = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void givePlayerMessage(string text)
    {
        StopAllCoroutines();
        playerText.text = "";
        increment = 0;
        StartCoroutine(sayMessage(text));
    }
    IEnumerator sayMessage(string text)
    {
        while(increment < text.Length)
        {
            yield return null;
        }
        
    }
}
