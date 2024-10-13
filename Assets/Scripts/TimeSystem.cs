using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Added for scene management

public class TimeSystem : MonoBehaviour
{
    public float countdownRate = 1f;
    public int attackMoneyIncreasePeriod = 10;

    private GameVariables gameVariables;
    private Calculation calculation;

    private TimeSpan remainingTime;
    private bool isCountingDown = true;
    private int initialHours;
    private int initialMinutes;
    private int initialSeconds;

    public void Init()
    {
        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
        calculation = GetComponent<Calculation>();
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
        StartCoroutine(CountdownTimer());
    }

    IEnumerator CountdownTimer()
    {
        int elapsedTime = 0;
        while (isCountingDown)
        {
            yield return new WaitUntil(() => gameVariables.systemInfo.pause == 0);
            if (remainingTime.TotalSeconds > 0)
            {
                yield return new WaitForSeconds(countdownRate);
                remainingTime = remainingTime.Subtract(new TimeSpan(0, 0, 1));
                gameVariables.systemInfo.currentTimeString = remainingTime.ToString();
                elapsedTime += 1;
                if (elapsedTime >= attackMoneyIncreasePeriod)
                {
                    calculation.ApplyAttackMoney();
                    elapsedTime = 0;
                }
            }
            else
            {
                isCountingDown = false;
                Debug.Log("Countdown end");
                // Load the DefenderWin scene when time reaches 0
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
