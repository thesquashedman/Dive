using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavelBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage = 10f;
    private bool hasEntered = false;
    public string[] enemyTags;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D hit) {
        Debug.Log("Bullet hit something");
        if (hit.collider != null && !hasEntered)
        {
            Debug.Log(hit.transform.name);
            hasEntered = true;
            
            // Check if the object hit has the tag "Enemy"
            if (enemyTags.Length > 0)
            {
                foreach (string tag in enemyTags)
                {
                    if (hit.collider.CompareTag(tag))
                    {
                        EventManager.current.DealDamageEnemy(hit.gameObject.GetInstanceID(), damage);
                    }
                }
            }
            
        }
        Destroy(gameObject);
    }
}
