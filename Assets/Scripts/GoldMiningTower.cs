using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMiningTower : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float goldGenerationInterval = 5f;
    [SerializeField] private int goldAmount = 10;
    [SerializeField] private LayerMask castleMask;
    
    private GameVariables gameVariables;

    private bool isActive = true;  // Flag to keep the tower active

    private void Start()
    {
        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
        StartCoroutine(GenerateGold());
    }

    private IEnumerator GenerateGold()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(goldGenerationInterval);

            // Add gold to defender's currency (assuming there's a GameManager handling currency)
            gameVariables.resourcesInfo.defenseMoney += goldAmount;
        }
    }
}