using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color selectedColor; // Color when a tower is selected for moving
    [SerializeField] private int relocationCost = 50;

    private GameObject tower;
    private Color startColor;
    public static int numberOfTurretsPlaced = 0;
    private static GameObject selectedTower = null; // Currently selected tower for moving
    private static Plot selectedPlot = null; // Plot containing the selected tower
    private GameVariables gameVariables;

    private void Start()
    {
        startColor = sr.color;
        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
    }

    private void OnMouseEnter()
    {
        // Highlight the plot on hover
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        // Reset the color when the mouse leaves
        sr.color = (selectedTower != null && selectedPlot == this) ? selectedColor : startColor;
    }

    private void OnMouseDown()
    {
        // Handle tower relocation if a tower is already selected
        if (selectedTower != null)
        {
            HandleTowerRelocation();
            return;
        }

        // Handle tower selection if there's a tower on this plot
        if (tower != null)
        {
            SelectTower();
            return;
        }

        // Handle normal tower placement
        PlaceOrReplaceTower();
    }

    private void HandleTowerRelocation()
    {
        if (gameVariables.resourcesInfo.defenseMoney < relocationCost)
        {
            BuildManager.main.popupManager.ShowMessage("You do not have enough money to relocate this tower!");
            Debug.Log("Not enough money to relocate tower!");
            return;
        }

        // Move the selected tower to an empty plot
        if (tower == null)
        {
            LevelManager.main.SpendCurrency(relocationCost);
            
            // Relocate the tower
            tower = selectedTower;
            tower.transform.position = transform.position;

            // Clear the old plot's tower
            selectedPlot.ClearTower();

            // Deselect the tower
            selectedTower = null;
            selectedPlot = null;

            Debug.Log("Tower successfully moved!");
        }
        else
        {
            Debug.Log("This plot is already occupied!");
        }
    }

    private void SelectTower()
    {
        selectedTower = tower;
        selectedPlot = this;
        sr.color = selectedColor; // Change color to indicate selection
        Debug.Log("Tower selected for moving!");
    }

    private void PlaceOrReplaceTower()
    {
        // Get the tower to build from the BuildManager
        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        // Return if no tower is selected to build
        if (towerToBuild == null) return;

        // Check if the player can afford the tower
        if (towerToBuild.cost > gameVariables.resourcesInfo.defenseMoney)
        {
            Debug.Log("Can't afford this tower!");
            return;
        }

        if (tower != null)
        {
            // Replace the existing tower if it's a different type
            if (tower.name != towerToBuild.prefab.name)
            {
                Destroy(tower); // Destroy the old tower
                LevelManager.main.SpendCurrency(towerToBuild.cost);
                tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
                Debug.Log("Replaced the existing tower with a new one!");
            }
            else
            {
                Debug.Log("Cannot place the same tower on this plot!");
            }
        }
        else
        {
            // Deduct currency and place the tower
            numberOfTurretsPlaced++;
            LevelManager.main.SpendCurrency(towerToBuild.cost);
            tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
            gameVariables.resourcesInfo.remainingTowers--;
        }
    }

    private void ClearTower()
    {
        // Clear the selected tower from the plot
        tower = null;
        sr.color = startColor;
    }
}
