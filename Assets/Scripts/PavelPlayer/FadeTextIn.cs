using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeTextIn : MonoBehaviour
{
    // Start is called before the first frame update
    float fadeInTime = 1f;
    public TextMeshProUGUI text;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.tag == "Player")
        {
            StopAllCoroutines();
            StartCoroutine(FadeInText());
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            StopAllCoroutines();
            StartCoroutine(FadeOutText());
        }
    }
    IEnumerator FadeInText()
    {
        while(text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / fadeInTime));
            yield return null;
        }
    }
    IEnumerator FadeOutText()
    {
        while(text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / fadeInTime));
            yield return null;
        }
    }
}
