using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefenderTMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI defenderCurrencyUI;

    private void OnGUI()
    {
        defenderCurrencyUI.text = TutorialLevelManager.main.tcurrency.ToString();
    }

    public void SetTSelected()
    {

    }

}
