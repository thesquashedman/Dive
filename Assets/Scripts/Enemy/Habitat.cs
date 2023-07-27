using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Habitat : MonoBehaviour
{
    // The circle that shows the wandering area of the fish.
    public GameObject circle;
    
    public FishEnemyBehavior fish;

    void Awake()
    {
        if (Application.isPlaying)
        {
            circle.SetActive(false);
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        circle.transform.localScale = new Vector3(fish.wanderingAreaWidth, fish.wanderingAreaHeight, 1);
    }
}
