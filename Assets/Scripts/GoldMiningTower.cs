using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMiningTower : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float goldGenerationInterval = 5f;  // Interval for generating gold
    [SerializeField] private int goldAmount = 10;  // Amount of gold generated
    [SerializeField] private LayerMask castleMask;  // Mask for detecting castles
    [SerializeField] private int health = 2;  // Health of the tower

    private GameVariables gameVariables;
    private PopUpManager defenderCurrencyIncreasePopup;
    private bool isActive = true;  // Flag to keep the tower active

    private void Start()
    {
        // Initializing game variables and pop-up manager
        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
        defenderCurrencyIncreasePopup = GameObject.Find("DefenderCurrencyIncreasePopup").GetComponent<PopUpManager>();

        // Start generating gold
        StartCoroutine(GenerateGold());
    }

    private IEnumerator GenerateGold()
    {
        while (isActive)
        {
            // Wait for the specified interval
            yield return new WaitForSeconds(goldGenerationInterval);

            // Add gold to defender's currency
            gameVariables.resourcesInfo.defenseMoney += goldAmount;
            defenderCurrencyIncreasePopup.ShowMessage("+" + goldAmount.ToString());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check if collided object is in the specified layer
        if (other.gameObject.layer != 6) return;

        // Damage the enemy if it has EnemyStats component
        EnemyStats enemyStats = other.gameObject.GetComponent<EnemyStats>();
        if (enemyStats != null)
        {
            enemyStats.TakeDamage(1000);
        }
        else
        {
            // Check for TutorialEnemyStats in tutorial level
            TutorialHealth tutorialEnemyStats = other.gameObject.GetComponent<TutorialHealth>();
            if (tutorialEnemyStats != null)
            {
                tutorialEnemyStats.TakeDamage(1000);
            }
        }

        // Decrease the health of the tower
        health--;
        // Destroy the tower if health reaches zero
        if (health == 0) Destroy(gameObject);
    }
}
