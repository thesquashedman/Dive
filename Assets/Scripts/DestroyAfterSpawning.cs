using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSpawning : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable() {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
