using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [Header("Start Points and Paths")]
    public Transform startPoint1;  
    public Transform[] path1;      

    public Transform startPoint2;  
    public Transform[] path2;      

    public Transform startPoint3;  
    public Transform[] path3;      

    private Transform selectedStartPoint;  
    private Transform[] selectedPath;      

    public int currency;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 100;

        // Set a default start point and path
        SetStartPoint(1); // Default to path 1
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetStartPoint(1);  
            Debug.Log("Path 1 selected.");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SetStartPoint(2);  
            Debug.Log("Path 2 selected.");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SetStartPoint(3);  // User pressed 'D', choose start point 3 and path 3
            Debug.Log("Path 3 selected.");
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
            Debug.Log("Not enough currency!");
            return false;
        }
    }
}

