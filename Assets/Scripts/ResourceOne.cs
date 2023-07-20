using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceOne : Resource
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            collision.gameObject.GetComponent<PlayerResourcesSystem>().ChangeResourceOne(1.0f);
            Destroy(this.gameObject);
        
        }
    }
}
