
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower; 
    private Color startColor;

    [SerializeField] private Color highlightColor = Color.yellow;
    private bool isHighlighted = false;
    private GameVariables gameVariables;

    
    private static GameObject movingTower = null;
    private static TutorialPlot originalPlot = null;

    public static HashSet<string> restrictedTileNames = new HashSet<string>
    {
        "TutorialPlot (50)", "TutorialPlot (86)", "TutorialPlot (74)",
        "TutorialPlot (71)", "TutorialPlot (117)", "TutorialPlot (141)",
        "TutorialPlot (105)", "TutorialPlot (59)", "TutorialPlot (95)", "TutorialPlot (80)", "TutorialPlot (78)"
    };

    private void OnMouseEnter()
    {
        if (!isHighlighted)
        {
            sr.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        if (!isHighlighted)
        {
            sr.color = startColor;
        }
    }

    public void ActivateHighlight()
    {
        if (sr != null && !isHighlighted)
        {
            sr.color = highlightColor;
            isHighlighted = true;
            StartCoroutine(DeactivateHighlightAfterDelay());
        }
    }

    public void DeactivateHighlight()
    {
        if (isHighlighted && sr != null)
        {
            sr.color = startColor;
            isHighlighted = false;
        }
    }

    private IEnumerator DeactivateHighlightAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        DeactivateHighlight();
    }

    private void Start()
    {
        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
        startColor = sr.color;
    }

    private void OnMouseDown()
    {
        
        if (movingTower != null)
        {
            HandleTowerPlacement();
            return;
        }

        
        if (tower != null)
        {
            StartMovingTower();
        }
        else
        {
            HandleNewTowerPlacement();
        }
    }

    private void HandleNewTowerPlacement()
    {
        
        if (movingTower != null)
        {
            Debug.Log("Cannot place a new tower while moving another tower!");
            return;
        }

        if (tower != null)
        {
            Debug.Log("Cannot place a new tower: This plot already has a tower!");
            return;
        }

        if (restrictedTileNames.Contains(gameObject.name))
        {
            Debug.Log("Cannot place a tower on a restricted tile!");
            return;
        }

        if (gameVariables.tutorialInfo.towerPlaceable == false)
        {
            Debug.Log("Tower placement is disabled!");
            return;
        }

        if (TutorialLevelManager.main.totalCount <= 0)
        {
            Debug.Log("Cannot place tower: totalCount is 0!");
            return;
        }

        TutorialTower tTowerToBuild = TutorialBuildManager.main.GetSelectedTTower();

        if (tTowerToBuild.tcost > TutorialLevelManager.main.tcurrency)
        {
            Debug.Log("Cannot afford this tower!");
            return;
        }

        
        TutorialLevelManager.main.tSpendCurrency(tTowerToBuild.tcost);
        tower = Instantiate(tTowerToBuild.perfabs, transform.position, Quaternion.identity);
        Debug.Log("Tower placed successfully!");
    }

    private void StartMovingTower()
    {
        
        movingTower = tower;
        originalPlot = this;
        tower = null; 
        Debug.Log("Tower is now movable.");
    }

    private void HandleTowerPlacement()
    {
        
        if (restrictedTileNames.Contains(gameObject.name))
        {
            Debug.Log("Cannot move tower to a restricted tile!");
            RestoreMovingTower();
            return;
        }

        if (tower != null)
        {
            Debug.Log("Cannot place tower here: Plot is already occupied!");
            RestoreMovingTower();
            return;
        }

        
        Debug.Log("Tower moved successfully.");
        movingTower.transform.position = transform.position;
        tower = movingTower;

        ResetMovingTower();
    }

    private void RestoreMovingTower()
    {
        
        Debug.Log("Restoring tower to its original position.");
        movingTower.transform.position = originalPlot.transform.position;
        originalPlot.tower = movingTower;
        ResetMovingTower();
    }

    private void ResetMovingTower()
    {
        
        movingTower = null;
        originalPlot = null;
    }
}



