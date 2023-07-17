using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextInteractable : MonoBehaviour
{
    // Start is called before the first frame update
    public float interactDistance = 1;
    public TextAsset textFile;
    public GameObject UIPanel;
    public TextMeshProUGUI text;
    void Start()
    {
        EventManager.current.onPlayerinteract += Interact;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Interact()
    {
        UIPanel.SetActive(true);
        text.SetText(textFile.text);
        LayoutRebuilder.ForceRebuildLayoutImmediate(text.rectTransform);
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }
    void DisplayText()
    {
        
    }
}
