using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialEnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] tenemyPrefabs;


    [Header("Attribute")]
    [SerializeField] private int tbaseEnemies = 8;
    [SerializeField] private float tenemiesPerSecond = 0.5f;
    [SerializeField] private float ttimeBetweenWaves = 0.2f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]

    public static UnityEvent onTEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float ttimeSinceLastSpwan;
    private int tenemiesAlive;
    private int tenemiesleftToSpwan;
    private bool isSpawning = false;
    private bool firstWave = true;

    private GameVariables gameVariables;
    private EnemySpawner enemyS;

    private void Awake()
    {
        onTEnemyDestroy.AddListener(EnemyDestroyed);
        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
    }
    private void Update()
    {if (!isSpawning) return;
        ttimeSinceLastSpwan += Time.deltaTime;
        int wave = gameVariables.systemInfo.wave;
        bool continueSpawn = gameVariables.systemInfo.continueSpawn;
        //if (currentWave == 2) { Debug.Log(1); };
        if (ttimeSinceLastSpwan >= (1f/ tenemiesPerSecond) && tenemiesleftToSpwan > 0 && continueSpawn == true)
        {
            tSpawnEnemy();
            tenemiesleftToSpwan--;
            tenemiesAlive++;
            ttimeSinceLastSpwan = 0f;
        }
      
        if (tenemiesAlive == 0 && tenemiesleftToSpwan == 0)
        {
            EndWave();
            gameVariables.systemInfo.wave += 1;
        }
    }

    private void EnemyDestroyed()
    {
        tenemiesAlive--;
    }
    private void tSpawnEnemy()
    {
        GameObject tprefabToSpwan = tenemyPrefabs[0];
        Instantiate(tprefabToSpwan, TutorialLevelManager.main.tstartPoint.position , Quaternion.identity);
    }

    private void EndWave()
    {
        isSpawning = false;
        ttimeSinceLastSpwan = 0f;
        currentWave++;
        StartCoroutine(StartWave());

    }
    private void Start()
    {
        StartCoroutine(StartWave());
        enemyS = FindObjectOfType<EnemySpawner>();
    }


    private IEnumerator StartWave()
    {
        /* yield return new WaitForSeconds(ttimeBetweenWaves);
         isSpawning = true;
         tenemiesleftToSpwan = tbaseEnemies;*/
        if (firstWave)
        {
            yield return new WaitForSeconds(4f);
            firstWave = false;
        }
        else
        {
            yield return new WaitForSeconds(ttimeBetweenWaves);
        }
        isSpawning = true;
        tenemiesleftToSpwan = tbaseEnemies;
    }
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(tbaseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}