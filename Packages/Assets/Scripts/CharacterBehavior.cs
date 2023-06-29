using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{
    public GameObject bulletPrefab;
    private float speed = 3f;

    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> bullets = new List<GameObject>();
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Fire a bullet if the space bar is clicked.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject temp = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullets.Add(temp);
        }
        
        // Receive input from the player.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move the character based on the input.
        Vector3 movement = new Vector3(horizontalInput * speed * Time.deltaTime, verticalInput * speed * Time.deltaTime, 0f);
        transform.Translate(movement);
    }
    

    // Incomplete code for expectimax (low prioirty).

    // Evaluate the game state by computing the heuristic. If the heuristic is
    // higher, the game state favors the main character. If it is lower,
    // the game state favors the enemies.
    /*
    float GetHeuristic(Vector3 characterPosition, List<Vector3> enemiesPositions, List<Vector3>bulletsPositions)
    {
        float heuristic = 0f;
        
        for (int i = 0; i < enemiesPositions.Count; i++)
        {
            // Compute the total distance from the main character to all the enemies.
            // Increase the heuristic by this distance.
            heuristic += Vector3.Distance(characterPosition, enemiesPositions[i]);
            
            // Compute the number of enemies that are hit by the bullets. Decrease the
            // heuristic by 10 for each of these.
            EnemyBehavior enemyBehavior = enemies[i].GetComponent<EnemyBehavior>();
            for (int j = 0; j < bulletsPositions.Count; j++)
            {
                if (Vector3.Distance(enemiesPositions[i], bulletsPositions[j]) <= enemyBehavior.GetRange())
                {
                    heuristic -= 10f;
                }
            }
        }
        
        // Return the total heuristic.
        return heuristic;
    }

    // Compute the best action for the enemy that calls this function using a
    // expectimax algorithm.
    public Vector3 GetBestEnemyAction()
    {
        return Vector3.zero;
    }

    // This is the helper method of the GetBestEnemyAction method; this helper method
    // performs the expectimax computation.
    float Value(GameState state, int depth, int index)
    {
        if (depth <= 0)
        {
            return GetHeuristic(state.characterPosition, state.enemiesPositions, state.bulletsPositions);
        }
        else
        {
            if (index < enemies.Count)
            {
                return MinValue(state, depth, index);
            }
            else if (index == enemies.Count)
            {
                return ExpectValue(state, depth, index);
            }
            else
            {
                return Value(state, depth - 1, 0);
            }
        }
    }
    
    float MinValue(GameState state, int depth, int index)
    {
        float result = float.MaxValue;
        foreach (Vector3 direction in enemies[index].GetComponent<EnemyBehavior>().GetLegalActions())
        {
            GameState newState = new GameState();
            newState.characterPosition = new Vector3(state.characterPosition.x, state.characterPosition.y, 0f);
            newState.enemiesPositions = new List<Vector3>(state.enemiesPositions);
            newState.bulletsPositions = new List<Vector3>(state.bulletsPositions);

            newState.enemiesPositions[index] += (direction * 0.5f);
            float temp = Value(newState, depth, index + 1);
            if (result > temp)
            {
                result = temp;
            }
        }

        return result;
    }

    float ExpectValue(GameState state, int depth, int index)
    {
        float result = 0f;
        foreach (Vector3 direction in enemies[index].GetComponent<EnemyBehavior>().GetLegalActions())
        {
            GameState newState = new GameState();
            newState.characterPosition = new Vector3(state.characterPosition.x, state.characterPosition.y, 0f);
            newState.enemiesPositions = new List<Vector3>(state.enemiesPositions);
            newState.bulletsPositions = new List<Vector3>(state.bulletsPositions);

            newState.enemiesPositions[index] += (direction * 0.5f);
            float temp = Value(newState, depth, index + 1);
            if (result > temp)
            {
                result = temp;
            }
        }

        return result;
    }
    */
}

// This class represents the game state.
/*
public class GameState
{
    public Vector3 characterPosition = new Vector3();
    public List<Vector3> enemiesPositions = new List<Vector3>();
    public List<Vector3> bulletsPositions = new List<Vector3>();
}
*/