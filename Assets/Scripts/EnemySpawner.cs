using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs; // Prefabs for enemy types
    [SerializeField] private PopUpManager popupManager;

    [Header("Attributes")]
    // [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int enemiesAlive = 0; // Track the number of alive enemies
    private GameVariables gameVariables;
    public int numberOfEnemiesSpawned = 0;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
    }

    private void Update()
    {
        
        // Check for button presses to spawn specific enemy types
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Button 1
        {
            Debug.Log("Spawning enemy type 1");
            SpawnEnemy(0); // Spawn enemy of type 1
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // Button 2
        {
            Debug.Log("Spawning enemy type 2");
            SpawnEnemy(1); // Spawn enemy of type 2
        }
        // Add more conditions here for additional enemy types as needed
    }

    private void EnemyDestroyed() // Called when an enemy is destroyed
    {
        enemiesAlive--;
    }

    private void SpawnEnemy(int enemyTypeIndex)
    {
        if (enemyTypeIndex < 0 || enemyTypeIndex >= enemyPrefabs.Length) return; // Validate index

        GameObject prefabToSpawn = enemyPrefabs[enemyTypeIndex];

        // Get the EnemyStats component from the prefab
        EnemyStats enemyStats = prefabToSpawn.GetComponent<EnemyStats>();
        enemyStats.startTime = Time.time;
        if (enemyStats == null)
        {
            Debug.LogError("EnemyStats component missing on prefab: " + prefabToSpawn.name);
            return;
        }

        // Check if there is enough attacker currency
        if (gameVariables.resourcesInfo.attackMoney >= enemyStats.cost)
        {
            // Deduct the cost from attacker currency
            gameVariables.resourcesInfo.attackMoney -= enemyStats.cost;

            // Instantiate the enemy
            Instantiate(prefabToSpawn, LevelManager.main.GetSelectedStartPoint().position, Quaternion.identity);
            enemiesAlive++; // Increment the count of alive enemies
            numberOfEnemiesSpawned++;
        }
        else
        {
            popupManager.ShowMessage("Not enough currency to spawn enemy type " + (enemyTypeIndex + 1));
        }
    }

}