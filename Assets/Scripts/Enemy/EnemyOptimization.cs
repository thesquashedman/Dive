using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOptimization : MonoBehaviour
{
    public GameObject player;

    // All the enemies in this section/level.
    public List<GameObject> enemies;

    // The distance from the player to the enemy that the enemy will be
    // activated/deactivated.
    public float activationDistance = 100f;

    private float timer = 0f;
    
    // The interval between each check.
    public float interval = 0.5f;

    // Start is called before the first frame update
    private void Start()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        // Activate/deactivate the enemies every interval.
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;

            int count = enemies.Count;
            for (int i = 0; i < count; i++)
            {
                GameObject enemy = enemies[i];
                float distance = Vector2.Distance(player.transform.position, enemy.transform.position);

                if (distance >= activationDistance && enemy.activeSelf)
                {
                    if (enemy.GetComponent<FishEnemyBehavior>().GetMode() == "dead")
                    {
                        enemies.RemoveAt(i);
                        i--;
                    }
                    enemy.gameObject.SetActive(false);
                }
                else if (distance < activationDistance && !enemy.activeSelf)
                {
                    enemy.gameObject.SetActive(true);
                }
            }
        }
    }
}
