using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialUIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI defenderTurnText; 
    [SerializeField] private GameObject turretImage;
    [SerializeField] private TextMeshProUGUI castleLifeText;
    [SerializeField] private TextMeshProUGUI totalCountText;

    [Header("Plot Highlight")]
    public TutorialPlot plot55; 

    private Color yellowColor = Color.yellow; 
    public UnityEngine.UI.Button towerButton;

    private void Start()
    {
        
        StartCoroutine(ShowDefenderTurnMessage());

        StartCoroutine(HandleTurretImageVisibility());
    }

    
    private IEnumerator ShowDefenderTurnMessage()
    {
        defenderTurnText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        defenderTurnText.gameObject.SetActive(false);


        HighlightPlot();
    }

    private IEnumerator HandleTurretImageVisibility()
    {
        if (turretImage != null)
        {
            turretImage.SetActive(false); 
            yield return new WaitForSeconds(3); 
            turretImage.SetActive(true); 
        }
    }


    
    private void HighlightPlot()
    {
        if (plot55 != null)
        {
            plot55.ActivateHighlight();
        }
    }

    
    public void RemoveHighlights()
    {
        
        if (plot55 != null)
        {
            plot55.DeactivateHighlight();
        }
    }
    public void HideTurretImage()
    {
        Debug.Log("Hide turret image");
        turretImage.SetActive(false);
        if (turretImage != null)
        {
            turretImage.SetActive(false);
        }
    }
    public void UpdateCastleLife(int life)
    {
        castleLifeText.text = life.ToString(); 
    }
    public void UpdateTotalCount(int count)
    {
        totalCountText.text = count.ToString(); 
    }

    public void DisableTowerButton()
    {
        if (towerButton != null)
        {
            towerButton.interactable = false; // Disable the button
            Debug.Log("Tower button disabled.");
        }
    }

}




