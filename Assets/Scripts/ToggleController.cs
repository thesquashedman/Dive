using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleController : MonoBehaviour
{
    public PavelPlayerHealth playerHealth;
    public GameObject lightEffect;
    private float interval = 0.3f;
    private float timer = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        lightEffect.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth.currentHealth <= 40f) {
            timer += Time.deltaTime;

            if (timer >= interval)
            {
                timer = 0f;
                lightEffect.SetActive(!lightEffect.activeSelf);
            }
        }
        else
        {
            timer = interval;
            lightEffect.SetActive(false);
        }
    }
}
