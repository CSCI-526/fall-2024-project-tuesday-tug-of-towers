using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    
    public int currency;  

    
    public int attackerCurrency = 200; 

    
    private int enemyCost = 25;

    
    private float currencyIncreaseInterval = 5f; 
    private int currencyIncreaseAmount = 10;

    public float timeLeft = 120f; 
    public TextMeshProUGUI timerText;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 100;  

        
        StartCoroutine(IncreaseAttackerCurrencyOverTime());
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime; 

            
            int minutes = Mathf.FloorToInt(timeLeft / 60);
            int seconds = Mathf.FloorToInt(timeLeft % 60);

            
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timeLeft = 0;
            timerText.text = "00:00"; 
            
        }
    }

    
    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)  
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("No money for defender");
            return false;
        }
    }

    
    public bool SpendAttackerCurrency(int amount)
    {
        if (amount <= attackerCurrency)
        {
            attackerCurrency -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not enough currency for attacker");
            return false;
        }
    }

    
    private IEnumerator IncreaseAttackerCurrencyOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(currencyIncreaseInterval);
            attackerCurrency += currencyIncreaseAmount;
            Debug.Log("Attacker currency increased by " + currencyIncreaseAmount + ", new total: " + attackerCurrency);
        }
    }

    
    public int GetEnemyCost()
    {
        return enemyCost;
    }

    
    public int GetAttackerCurrency()
    {
        return attackerCurrency;
    }
}


