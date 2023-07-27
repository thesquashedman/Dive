using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeSound : MonoBehaviour
{
    protected AudioSource audioSource;
    public string audioName;

    // Whether this sound have been triggered.
    private bool triggered = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        if (audioSource.clip == null)
        {
            AudioManager.instance.SetSource(audioName, audioSource);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        // Play the sound if the player enters the trigger.
        if (other.CompareTag("Player") && !triggered)
        {
            audioSource.Play();
            triggered = true;
        }
    }

    public void Reset()
    {
        triggered = false;
    }
}
