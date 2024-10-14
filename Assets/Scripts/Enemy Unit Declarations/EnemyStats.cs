using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float hitPoints = 10; //2 points to hit before it gets destroyed
    [SerializeField] private int currencyWorth = 50;
    [SerializeField] public int cost = 20;

    private bool isDestroyed = false;

    public void TakeDamage(float dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
