using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Added for scene management

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    public GoogleFormSubmit googleFormSubmit;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 4f;
    private EnemySpawner spawner;
    int turretsPlaced;

    private Transform target;
    private int pathIndex = 0;

    private float baseSpeed;
    private Transform[] currentPath;  // Store the current path
    private GameVariables gameVariables;

    // Static variable to count how many enemies have reached the end of the path
    private static int enemiesReachedEnd = 0; // Counts enemies that reach the end

    private void Start()
    {
        // Reset enemiesReachedEnd at the start of a new game
        enemiesReachedEnd = 0;

        baseSpeed = moveSpeed;

        // Get the currently selected path from LevelManager
        currentPath = LevelManager.main.GetSelectedPath();
        target = currentPath[pathIndex];

        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
        spawner = FindObjectOfType<EnemySpawner>();

        GameObject managerObj = GameObject.Find("GoogleFormManager");

        if (managerObj != null)
        {
            // Get the GoogleFormSubmit component from the GameObject
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
            Debug.LogError("GoogleFormManager GameObject not found!");
        }
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == currentPath.Length)
            {
                enemiesReachedEnd++; // Increment the count of enemies that reached the end

                // Check if the count reaches 10 to load the AttackerWin scene
                if (enemiesReachedEnd >= 10)
                {
                    turretsPlaced = Plot.numberOfTurretsPlaced;
                    String SessionID = DateTime.UtcNow.Ticks.ToString();
                    if (googleFormSubmit != null)
                    {
                        // Example data to send
                        string sessionId = SessionID;
                        string winner = "Attacker";
                        int numAttackers = spawner.numberOfEnemiesSpawned;
                        int numTurrets = turretsPlaced;

                        // Call the SubmitData function
                        googleFormSubmit.SubmitData(sessionId, winner, numAttackers, numTurrets);
                    }
                    else
                    {
                        Debug.LogError("GoogleFormSubmit component not assigned!");
                    }
                    SceneManager.LoadScene("AttackerWin"); // Load the AttackerWin scene
                }

                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject); // Destroy the game object when reaching the end
                DefenseLifeDecrease(1);
                return;
            }
            else
            {
                target = currentPath[pathIndex];
            }
        }
    }

    private void DefenseLifeDecrease(int point)
    {
        gameVariables.resourcesInfo.defenseLife -= point;
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }
}
