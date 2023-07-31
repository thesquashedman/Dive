using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeEnemySpawn : MonoBehaviour
{
    public GameObject enemyToSpawn;

    // Whether this enemy have been triggered.
    private bool triggered = false;

    // Reference to the optimization script.
    public EnemyOptimization optimization;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        enemyToSpawn.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        // Spawn the enemy if the player enters the trigger.
        if (other.CompareTag("Player") && !triggered)
        {
            enemyToSpawn.SetActive(true);
            optimization.enemies.Add(enemyToSpawn);
            triggered = true;
        }
    }
}
