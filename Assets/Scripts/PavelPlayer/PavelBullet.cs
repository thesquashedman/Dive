using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavelBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 10f;
    public string[] enemyTags;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D hit) {
        if (hit.collider != null)
        {

            Debug.Log(hit.transform.name);
            // Check if the object hit has the tag "Enemy"
            if (enemyTags.Length > 0)
            {
                foreach (string tag in enemyTags)
                {
                    if (hit.collider.CompareTag(tag))
                    {
                        // Get the Health component and call ChangeHealth
                        Health enemyHealth = hit.collider.GetComponent<Health>();
                        if (enemyHealth != null)
                        {
                            
                            enemyHealth.ChangeHealth(-damage);
                        }
                    }
                }
            }
            
        }
        Destroy(gameObject);
    }
}
