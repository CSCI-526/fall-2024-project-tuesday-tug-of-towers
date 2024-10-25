using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Added for scene management

public class TimeSystem : MonoBehaviour
{
    public float countdownRate = 1f;
    public GoogleFormSubmit googleFormSubmit;
    [SerializeField] private PopUpManager CurrencyIncreasePupup;

    private GameVariables gameVariables;
    private Calculation calculation;
    private TimeSpan remainingTime;
    private bool isCountingDown = true;
    private int initialHours;
    private int initialMinutes;
    private int initialSeconds;
    private float attackMoneyIncreasePeriod;
    private EnemySpawner spawner;
    int turretsPlaced;

    public void Init()
    {
        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
        calculation = GetComponent<Calculation>();
        spawner = FindObjectOfType<EnemySpawner>();
        turretsPlaced = Plot.numberOfTurretsPlaced;
        string[] timeParts = gameVariables.systemInfo.currentTimeString.Split(':');
        if (timeParts.Length == 3)
        {
            // Convert the string values into integers
            if (int.TryParse(timeParts[0], out int hours) && int.TryParse(timeParts[1], out int minutes) && int.TryParse(timeParts[2], out int seconds))
            {
                initialHours = hours;
                initialMinutes = minutes;
                initialSeconds = seconds;
            }
            else
            {
                Debug.LogError("Invalid time format in currentTimeString.");
            }
        }
        else
        {
            Debug.LogError("Invalid time format in currentTimeString.");
        }
        remainingTime = new TimeSpan(initialHours, initialMinutes, initialSeconds);
        attackMoneyIncreasePeriod = gameVariables.statisticsInfo.attackMoneyIncreasePeriod;
        StartCoroutine(CountdownTimer());
    }

    IEnumerator CountdownTimer()
    {
        float elapsedTime = 0f;
        while (isCountingDown)
        {
            yield return new WaitUntil(() => gameVariables.systemInfo.pause == 0);
            if (remainingTime.TotalSeconds > 0)
            {
                yield return new WaitForSeconds(countdownRate);
                remainingTime = remainingTime.Subtract(new TimeSpan(0, 0, 1));
                gameVariables.systemInfo.currentTimeString = remainingTime.ToString();
                elapsedTime += countdownRate;
                if (elapsedTime >= attackMoneyIncreasePeriod)
                {
                    if (gameVariables.resourcesInfo.attackMoney + gameVariables.statisticsInfo.attackMoneyRate >= ResourcesInfo.maxAttackMoney)
                        CurrencyIncreasePupup.ShowMessage("+" + (ResourcesInfo.maxAttackMoney - gameVariables.resourcesInfo.attackMoney));
                    else
                        CurrencyIncreasePupup.ShowMessage("+" + (gameVariables.statisticsInfo.attackMoneyRate));

                    calculation.ApplyAttackMoney();
                    elapsedTime = 0;
                }
            }
            else
            {
                isCountingDown = false;
                Debug.Log("Countdown end");
                // Load the DefenderWin scene when time reaches 0
                turretsPlaced = Plot.numberOfTurretsPlaced;
                String SessionID = DateTime.UtcNow.Ticks.ToString();
                if (googleFormSubmit != null)
                {
                    // Example data to send
                    string sessionId = SessionID;
                    string winner = "Defender";
                    int numAttackers = spawner.numberOfEnemiesSpawned;
                    int numTurrets = turretsPlaced;

                    // Call the SubmitData function
                    googleFormSubmit.SubmitData(sessionId, winner, numAttackers, numTurrets);
                }
                else
                {
                    Debug.LogError("GoogleFormSubmit component not assigned!");
                }
                SceneManager.LoadScene("DefenderWin"); // Added line to load the DefenderWin scene
            }
        }
    }

    public void TogglePause()
    {
        gameVariables.systemInfo.pause = gameVariables.systemInfo.pause == 0 ? 1 : 0;
        gameVariables.systemInfo.pauseShow = gameVariables.systemInfo.pause == 1 ? "Paused" : "Unpaused";
    }
}
