using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI AttackercurrencyUI;


    private void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();
        AttackercurrencyUI.text = LevelManager.main.attackerCurrency.ToString();
    }

    public void SetSelected()
    {

    }
}
