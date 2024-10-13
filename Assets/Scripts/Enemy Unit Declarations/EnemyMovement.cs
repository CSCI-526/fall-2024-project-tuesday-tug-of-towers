using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameVariables gameVariables;

    private void Start()
    {
        baseSpeed = moveSpeed;

        // Get the currently selected path from LevelManager
        currentPath = LevelManager.main.GetSelectedPath();
        target = currentPath[pathIndex];
        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == currentPath.Length)
            {
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

