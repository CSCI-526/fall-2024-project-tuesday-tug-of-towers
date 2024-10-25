using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;
    public static int numberOfTurretsPlaced = 0;
    private GameVariables gameVariables;

    private void Start()
    {
        startColor = sr.color;
        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (tower != null) return;

        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        if (towerToBuild == null)
            return;

        if(towerToBuild.cost > gameVariables.resourcesInfo.defenseMoney)
        {
            Debug.Log("Can't afford this");
            return;
        }
        numberOfTurretsPlaced++;
        //Debug.Log(numberOfTurretsPlaced);
        LevelManager.main.SpendCurrency(towerToBuild.cost);

        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        BuildManager.main.placedTowerCount++;
    }
}
