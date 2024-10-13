using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    //public GoogleFormSubmit formSubmitter;


    [Header("Start Points and Paths")]
    public Transform startPoint1;  
    public Transform[] path1;      

    public Transform startPoint2;  
    public Transform[] path2;      

    public Transform startPoint3;  
    public Transform[] path3;      

    private Transform selectedStartPoint;  
    private Transform[] selectedPath;      

    public int defenderCurrency;
    public int attackerCurrency;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        defenderCurrency = 100;
        attackerCurrency = 100;

        // Set a default start point and path
        SetStartPoint(1); // Default to path 1
        //string sessionId = "Session123";
        //string winner = "Attacker";  // Options: "Attacker" or "Defender"
        //int numAttackers = 25;
        //int numTurrets = 10;

        //// Submit the data to Google Form
        //formSubmitter.OnGameEnd(sessionId, winner, numAttackers, numTurrets);
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetStartPoint(3);
            Debug.Log("Path 3 selected.");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SetStartPoint(2);
            Debug.Log("Path 2 selected.");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SetStartPoint(1);
            Debug.Log("Path 1 selected.");
        }
    }

    
    public void SetStartPoint(int point)
    {
        if (point == 1)
        {
            selectedStartPoint = startPoint1;
            selectedPath = path1;
        }
        else if (point == 2)
        {
            selectedStartPoint = startPoint2;
            selectedPath = path2;
        }
        else if (point == 3)
        {
            selectedStartPoint = startPoint3;
            selectedPath = path3;
        }
    }

    
    public Transform GetSelectedStartPoint()
    {
        return selectedStartPoint;
    }

    
    public Transform[] GetSelectedPath()
    {
        return selectedPath;
    }

    public void DecreaseAttackerCurrency(int amount)
    {
        attackerCurrency -= amount;
        Debug.Log("new attacker currency is " + attackerCurrency);
    }

    public void IncreaseCurrency(int amount)
    {
        defenderCurrency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= defenderCurrency)
        {
            defenderCurrency -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not enough currency!");
            return false;
        }
    }
}

