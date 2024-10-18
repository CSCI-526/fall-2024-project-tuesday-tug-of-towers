using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private float spawnDelay = 0.5f;
    private bool canSpawn = true;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int enemiesAlive = 0;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1) && canSpawn)
        {
            
            int currentEnemyCost = LevelManager.main.GetEnemyCost();
            int attackerCurrency = LevelManager.main.GetAttackerCurrency();

            if (attackerCurrency >= currentEnemyCost)
            {
                
                if (LevelManager.main.SpendAttackerCurrency(currentEnemyCost))
                {
                    StartCoroutine(SpawnEnemyWithDelay());
                }
            }
            else
            {
                Debug.Log("Not enough currency to spawn an enemy. Required: " + currentEnemyCost + ", Available: " + attackerCurrency);
            }
        }
    }

    private IEnumerator SpawnEnemyWithDelay()
    {
        canSpawn = false;

        SpawnEnemy();
        enemiesAlive++;

        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }

    private void SpawnEnemy()
    {
        int index = 0;
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }
}





