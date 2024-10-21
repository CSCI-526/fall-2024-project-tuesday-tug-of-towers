using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f; // Set the desired constant speed here

    private Transform target;
    private int pathIndex = 0;

    private static int enemiesReachedEnd = 0;
    private const int maxEnemiesAllowed = 4;

    private void Start()
    {
        target = LevelManager.main.path[pathIndex];
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length)
            {
                enemiesReachedEnd++;
     
                if (enemiesReachedEnd >= maxEnemiesAllowed)
                {
                    Debug.Log("ATTACKER WINS!");
                }

                EnemySpawner.onEnemyDestroy.Invoke(); 
                Destroy(gameObject); 
                return;
            }
            else
            {
                
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed; 
    }
}


