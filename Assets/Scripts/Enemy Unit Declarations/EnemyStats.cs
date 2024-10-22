using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float hitPoints = 10;
    [SerializeField] private int currencyWorth = 50;
    [SerializeField] public int cost = 20;

    [Header("Floating Text")]
    public GameObject floatingTextPrefab;

    private bool isDestroyed = false;

    public void TakeDamage(float dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            ShowFloatingText();
            Destroy(gameObject);
        }
    }

    void ShowFloatingText()
    {
        if (floatingTextPrefab != null)
        {
            Vector3 enemyPosition = transform.position;
            GameObject floatingText = Instantiate(floatingTextPrefab, enemyPosition, Quaternion.identity);

            FloatingText floatingTextScript = floatingText.GetComponent<FloatingText>();
            if (floatingTextScript != null)
            {
                floatingTextScript.SetWorldPosition(enemyPosition);
                floatingTextScript.SetText("+" + currencyWorth.ToString());
            }
        }
        else
        {
            Debug.LogError("Floating Text Prefab is not assigned.");
        }
    }

}
