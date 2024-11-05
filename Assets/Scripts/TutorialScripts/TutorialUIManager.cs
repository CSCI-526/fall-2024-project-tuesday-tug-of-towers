using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialUIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI InstructionTxt; 
    [SerializeField] private GameObject turretImage;
    [SerializeField] private TextMeshProUGUI castleLifeText;
    [SerializeField] private TextMeshProUGUI totalCountText;
    [SerializeField] private GameObject mask0;
    [SerializeField] private GameObject mask1;
    [SerializeField] private Button defenderConfirmButton;

    [Header("Plot Highlight")]
    public TutorialPlot plot55; 

    private Color yellowColor = Color.yellow; 
    public UnityEngine.UI.Button towerButton;

    private GameVariables gameVariables;

    private enum TutorialStep
    {
        RoleDecision,
        DefenderIntro,
        DefenderTutorial,
        ShowTowerSelection,
        PlaceTower,
        AttackerIntro,
        End
    }
    private TutorialStep currentStep = TutorialStep.RoleDecision;

    private void Start()
    {
        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
        AdvanceStep(); 
    }

    private void AdvanceStep()
    {
        switch (currentStep)
        {
            case TutorialStep.RoleDecision:
                StartCoroutine(RoleDecision());
                currentStep = TutorialStep.DefenderIntro;
                break;
            case TutorialStep.DefenderIntro:
                StartCoroutine(DefenderIntro());
                currentStep = TutorialStep.DefenderTutorial;
                break;
            case TutorialStep.DefenderTutorial:
                StartCoroutine(DefenderTutorial());
                currentStep = TutorialStep.ShowTowerSelection;
                break;
            case TutorialStep.ShowTowerSelection:
                StartCoroutine(HandleTurretImageVisibility());
                currentStep = TutorialStep.AttackerIntro;
                //currentStep = TutorialStep.PlaceTower;
                break;
            case TutorialStep.PlaceTower:
                HighlightPlot();
                currentStep = TutorialStep.AttackerIntro;
                break;
            case TutorialStep.AttackerIntro:
                StartCoroutine(AttackerIntro());
                currentStep = TutorialStep.End;
                break;
            case TutorialStep.End:
                BackToMain();
                break;
        }
    }


    bool isPanel1Confirmed = false;
    bool isPanel0Confirmed = false;

    private void BackToMain()
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator RoleDecision()
    {
        mask0.SetActive(true);
        Transform panel0 = mask0.transform.Find("Panel0");
        Transform panel1 = mask0.transform.Find("Panel1");
        GameObject panel0Confirm = panel0.Find("Confirm")?.gameObject;
        GameObject panel1Confirm = panel1.Find("Confirm")?.gameObject;
        Button panel0InstructButton = panel0.Find("instruct")?.GetComponent<Button>();
        TextMeshProUGUI panel0InstructText = panel0InstructButton?.GetComponentInChildren<TextMeshProUGUI>();
        Button panel1InstructButton = panel1.Find("instruct")?.GetComponent<Button>();
        TextMeshProUGUI panel1InstructText = panel1InstructButton?.GetComponentInChildren<TextMeshProUGUI>();

        panel1Confirm.SetActive(false);
        panel0Confirm.SetActive(false);

        panel1InstructButton.onClick.AddListener(() =>
        {
            isPanel1Confirmed = !isPanel1Confirmed;
            panel1Confirm.SetActive(isPanel1Confirmed);
            panel1InstructText.text = isPanel1Confirmed ? "Click to Cancel" : "Click to Confirm";
            StartCoroutine(CheckAndAdvanceWithDelay());
        });

        StartCoroutine(WaitForKeyPress(panel0Confirm, panel0InstructText));
        yield return null;
    }

    private IEnumerator WaitForKeyPress(GameObject panel0Confirm, TextMeshProUGUI panel0InstructText)
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                isPanel0Confirmed = !isPanel0Confirmed;
                panel0Confirm.SetActive(isPanel0Confirmed);
                panel0InstructText.text = isPanel0Confirmed ? "Type 1 to Cancel" : "Type 1 to Confirm";
                StartCoroutine(CheckAndAdvanceWithDelay());
            }
            yield return null;
        }
    }
    private IEnumerator CheckAndAdvanceWithDelay()
    {
        if (isPanel0Confirmed && isPanel1Confirmed)
        {
            yield return new WaitForSeconds(1);
            mask0.SetActive(false);
            AdvanceStep();
        }
    }


    private IEnumerator DefenderIntro()
    {
        Transform panel1 = mask1.transform.Find("Panel1");
        GameObject InstructButton = panel1.Find("instruct")?.gameObject;
        TextMeshProUGUI InstructText = InstructButton?.GetComponentInChildren<TextMeshProUGUI>();
        Button NextButton = panel1.Find("next")?.GetComponent<Button>();

        mask1.SetActive(true);
        InstructText.text = "Now is Defender Tutorial";
        yield return new WaitForSeconds(3);
        NextButton.gameObject.SetActive(true);

        bool buttonClicked = false;
        NextButton.onClick.AddListener(() => buttonClicked = true);
        yield return new WaitUntil(() => buttonClicked);
        NextButton.gameObject.SetActive(false);

        InstructText.text = "Click the tower and build it on the map.";
        yield return new WaitForSeconds(3);
        NextButton.gameObject.SetActive(true);

        buttonClicked = false;
        NextButton.onClick.AddListener(() => buttonClicked = true);
        yield return new WaitUntil(() => buttonClicked);
        NextButton.gameObject.SetActive(false);

        mask1.SetActive(false);
        AdvanceStep();
        yield return null;

    }

    private IEnumerator DefenderTutorial()
    {
        yield return new WaitForSeconds(1);
        HighlightPlot();
        AdvanceStep();
    }

    private IEnumerator AttackerIntro()
    {
        Transform panel1 = mask1.transform.Find("Panel1");
        GameObject InstructButton = panel1.Find("instruct")?.gameObject;
        TextMeshProUGUI InstructText = InstructButton?.GetComponentInChildren<TextMeshProUGUI>();
        Button NextButton = panel1.Find("next")?.GetComponent<Button>();

        yield return new WaitForSeconds(1);
        mask1.SetActive(true);
        InstructText.text = "Now is Attacker Tutorial";
        yield return new WaitForSeconds(3);
        NextButton.gameObject.SetActive(true);

        bool buttonClicked = false;
        NextButton.onClick.AddListener(() => buttonClicked = true);
        yield return new WaitUntil(() => buttonClicked);
        NextButton.gameObject.SetActive(false);

        InstructText.text = "Press keyboard button 1 or 2 to spawn Enemies.";
        yield return new WaitForSeconds(3);
        NextButton.gameObject.SetActive(true);

        buttonClicked = false;
        NextButton.onClick.AddListener(() => buttonClicked = true);
        yield return new WaitUntil(() => buttonClicked);
        NextButton.gameObject.SetActive(false);

        InstructText.text = "End of Tutorial, back to Main Menu.";
        yield return new WaitForSeconds(3);
        NextButton.gameObject.SetActive(true);

        buttonClicked = false;
        NextButton.onClick.AddListener(() => buttonClicked = true);
        yield return new WaitUntil(() => buttonClicked);
        NextButton.gameObject.SetActive(false);

        mask1.SetActive(false);
        AdvanceStep();
        yield return null;

    }

    private IEnumerator HandleTurretImageVisibility()
    {
        if (turretImage != null)
        {
            turretImage.SetActive(false); 
            yield return new WaitForSeconds(3); 
            turretImage.SetActive(true);
            //AdvanceStep();
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
        if (count == 0)
        {
            gameVariables.systemInfo.continueSpawn = false;
            AdvanceStep();
        }
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




