using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    //[SerializeField] private GameObject[] towerPrefabs;
    public Tower[] towers;
    [SerializeField] private PopUpManager popupManager;
    private GameVariables gameVariables;

    private int selectedTower = 0;

    private void Awake()
    {
        main = this;
        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
    }

    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    public void SetSelectedTower(int _selectedTower)
    {
        if (towers[_selectedTower].cost <= gameVariables.resourcesInfo.defenseMoney)
        {
            selectedTower = _selectedTower;
        }
        else
        {
            popupManager.ShowMessage("Not enough currency to spawn " + (towers[_selectedTower].name));
        }
    }
}
