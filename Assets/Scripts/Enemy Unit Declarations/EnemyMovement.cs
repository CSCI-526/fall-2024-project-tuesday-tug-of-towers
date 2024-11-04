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
    public TimeSystem timeSystem;

    private void Start()
    {
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
            if(SceneManager.GetActiveScene().name == "Main")
            {
                Debug.LogError("GoogleFormManager GameObject not found!");
            }
        }
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == currentPath.Length)
            {
                // Enemy reached the end of the path
                DefenseLifeDecrease(1);

                // Check if defense life has reached zero
                if (gameVariables.resourcesInfo.defenseLife <= 0)
                {
                    timeSystem = FindObjectOfType<TimeSystem>();
                    if (timeSystem != null)
                    {
                        timeSystem.FormSubmit("Attacker");
                    }
                    else
                    {
                        Debug.Log("Not assigned");
                    }

                    SceneManager.LoadScene("AttackerWin"); // Load the AttackerWin scene
                }


                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject); // Destroy the game object when reaching the end
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
