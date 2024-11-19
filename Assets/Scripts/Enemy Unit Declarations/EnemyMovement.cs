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
    private EnemyStats enemyStats;
    public GameObject systemsObject;
    public TimeSystem timeSystem;
    private void Start()
    {
        baseSpeed = moveSpeed;
        systemsObject = GameObject.Find("IndependentSystems");
        if (systemsObject != null)
        {
            timeSystem = systemsObject.GetComponent<TimeSystem>();
            if (timeSystem != null)
            {
                Debug.Log("Successfully accessed TimeSystem.");
            }
            else
            {
                Debug.LogError("TimeSystem component not found on IndependentSystems GameObject!");
            }
        }
        else
        {
            Debug.LogError("IndependentSystems GameObject not found!");
        }
        // Get the currently selected path from LevelManager
        currentPath = LevelManager.main.GetSelectedPath();
        target = currentPath[pathIndex];

        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
        spawner = FindObjectOfType<EnemySpawner>();
        enemyStats = GetComponent<EnemyStats>();
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
            if (SceneManager.GetActiveScene().name == "Main")
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
                DefenseLifeDecrease(1);

                // Check if defense life has reached zero
                Debug.Log(gameVariables.resourcesInfo.defenseLife);
                if (gameVariables.resourcesInfo.defenseLife <= 0)
                {
                    
                    timeSystem.FormSubmit("Attacker");
                    Debug.Log("Here");
                    SceneManager.LoadScene("AttackerWin"); // Load the AttackerWin scene
                }
                float timeAlive = Time.time - enemyStats.startTime;
                if (enemyStats.currencyWorth > 60)
                {
                    timeSystem.type2EnemyTime.Add(timeAlive);
                }
                else
                {
                    timeSystem.type1EnemyTime.Add(timeAlive);
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