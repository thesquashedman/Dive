using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMuzzleEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onPlayPlayerRecoil += Play;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Play()
    {
        GetComponent<ParticleSystem>().Play();
    }
}
