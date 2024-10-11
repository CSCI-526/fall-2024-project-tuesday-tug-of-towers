using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeSystem : MonoBehaviour
{
    public float countdownRate = 1f;
    public int initialHours;
    public int initialMinutes;
    public int initialSeconds;

    private GameVariables gameVariables;
    private TimeSpan remainingTime;
    private bool isCountingDown = true;


    public void Init()
    {
        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
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
        while (isCountingDown)
        {
            yield return new WaitUntil(() => gameVariables.systemInfo.pause == 0);
            if (remainingTime.TotalSeconds > 0)
            {
                //Debug.Log(remainingTime);
                yield return new WaitForSeconds(countdownRate);
                remainingTime = remainingTime.Subtract(new TimeSpan(0, 0, 1));
                gameVariables.systemInfo.currentTimeString = remainingTime.ToString();
            }
            else
            {
                isCountingDown = false;
                Debug.Log("Countdown end");
            }
        }
    }

    public void TogglePause()
    {
        gameVariables.systemInfo.pause = gameVariables.systemInfo.pause == 0 ? 1 : 0;
        gameVariables.systemInfo.pauseShow = gameVariables.systemInfo.pause == 1 ? "Paused" : "Unpaused";
    }
}
