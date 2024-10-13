using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Added for scene management

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;

    private float baseSpeed;
    private Transform[] currentPath;  // Store the current path

    // Static variable to count how many enemies have reached the end of the path
    private static int enemiesReachedEnd = 0;

    private void Start()
    {
        baseSpeed = moveSpeed;

        // Get the currently selected path from LevelManager
        currentPath = LevelManager.main.GetSelectedPath();
        target = currentPath[pathIndex];
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == currentPath.Length)
            {
                enemiesReachedEnd++; // Increment the count of enemies that reached the end

                // Check if the count reaches 4 to load the AttackerWin scene
                if (enemiesReachedEnd >= 4)
                {
                    SceneManager.LoadScene("AttackerWin"); // Load the AttackerWin scene
                }

                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject); // Destroy the game object when reaching the end
                return;
            }
            else
            {
                target = currentPath[pathIndex];
            }
        }
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
