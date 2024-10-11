using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI defenderCurrencyUI;
    [SerializeField] TextMeshProUGUI attackerCurrencyUI;


    private void OnGUI()
    {
        defenderCurrencyUI.text = LevelManager.main.defenderCurrency.ToString();
        attackerCurrencyUI.text = LevelManager.main.attackerCurrency.ToString();
    }

    public void SetSelected()
    {

    }
}
