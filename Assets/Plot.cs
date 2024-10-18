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

    private void Start()
    {
        startColor = sr.color;
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
        /*if (tower != null) return;



        Tower towerToBuild = BuildManager.main.GetSelectedTower();
        
        if (towerToBuild.cost> LevelManager.main.currency)
        {
            Debug.Log("Can't afford this");
            return;
        }
        LevelManager.main.SpendCurrency(towerToBuild.cost);

        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
         
         */

        Tower selectedTower = BuildManager.main.GetSelectedTower();

        if (selectedTower == null)
        {
            
            Debug.Log("No turret selected, please select one first.");
            return; 
        }

        if (selectedTower.cost > LevelManager.main.currency)
        {
            Debug.Log("Can't afford this");
            return;
        }
        LevelManager.main.SpendCurrency(selectedTower.cost);

        tower = Instantiate(selectedTower.prefab, transform.position, Quaternion.identity);


    }
}
