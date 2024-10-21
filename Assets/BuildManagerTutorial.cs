using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private Tower[] towers;

    private int? selectedTower = null; 

    private void Awake()
    {
        main = this;
    }

    public Tower? GetSelectedTower()
    {
        if (selectedTower.HasValue)
        {
            return towers[selectedTower.Value]; 
        }
        
        Debug.Log("No turret selected, please select one first.");
        return null; 
    }

    public void SetSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
    }

    public void DeselectTower()
    {
        selectedTower = null; 
    }
}


