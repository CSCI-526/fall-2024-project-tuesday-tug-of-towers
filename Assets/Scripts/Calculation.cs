using System.Collections;
using UnityEngine;

public class Calculation : MonoBehaviour
{
    public int attackMoneyRate = 50;
    private GameVariables gameVariables;

    private void Start()
    {
        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
    }

    public void ApplyAttackMoney()
    {
        gameVariables.resourcesInfo.attackMoney += attackMoneyRate;
    }
}
