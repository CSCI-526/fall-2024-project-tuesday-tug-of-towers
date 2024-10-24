using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For scene management

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    public GoogleFormSubmit googleFormSubmit;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 4f;

    private EnemySpawner spawner;
    private Transform target;
    private Transform[] currentPath; // Active path array

    private GameVariables gameVariables;
    private float baseSpeed;
    private int pathIndex = 0;
    int turretsPlaced;

    private void Start()
    {
        // Initialize speed and path settings
        baseSpeed = moveSpeed;
        currentPath = LevelManager.main.GetSelectedPath();
        target = currentPath[pathIndex];

        // Retrieve game variables and spawner references
        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
        spawner = FindObjectOfType<EnemySpawner>();

        GameObject managerObj = GameObject.Find("GoogleFormManager");

        if (managerObj != null)
        {
            // Assign GoogleFormSubmit component
            googleFormSubmit = managerObj.GetComponent<GoogleFormSubmit>();

            if (googleFormSubmit != null)
            {
                Debug.Log("GoogleFormManager successfully linked to EnemyMovement!");
            }
            else
            {
                Debug.LogError("GoogleFormSubmit component not found on GoogleFormManager!");
            }
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "Main")
            {
                Debug.LogError("GoogleFormManager GameObject not found!");
            }
        }
    }

    private void Update()
    {
        // Check if the enemy has reached the current target waypoint
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            // Check if enemy reached the final point in the path
            if (pathIndex == currentPath.Length)
            {
                DefenseLifeDecrease(1);

                // Verify if defense life has been depleted
                if (gameVariables.resourcesInfo.defenseLife <= 0)
                {
                    // Submit game session details
                    turretsPlaced = Plot.numberOfTurretsPlaced;
                    string sessionId = DateTime.UtcNow.Ticks.ToString();

                    if (googleFormSubmit != null)
                    {
                        string winner = "Attacker";
                        int numAttackers = spawner.numberOfEnemiesSpawned;
                        int numTurrets = turretsPlaced;

                        googleFormSubmit.SubmitData(sessionId, winner, numAttackers, numTurrets);
                    }
                    else
                    {
                        Debug.LogError("GoogleFormSubmit component not assigned!");
                    }

                    SceneManager.LoadScene("AttackerWin"); // Load victory scene for attackers
                }

                // Destroy enemy upon reaching the end
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
            }
            else
            {
                // Move to the next target in the path
                target = currentPath[pathIndex];
            }
        }
    }

    private void DefenseLifeDecrease(int point)
    {
        // Decrease defense life by specified points
        gameVariables.resourcesInfo.defenseLife -= point;
    }

    private void FixedUpdate()
    {
        // Update velocity towards the target
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    public void UpdateSpeed(float newSpeed)
    {
        // Adjust movement speed
        moveSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        // Reset speed to base speed
        moveSpeed = baseSpeed;
    }
}
