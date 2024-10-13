using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public GameObject swapPopUp;
    private bool isPaused = false;

    private void Start()
    {
        swapPopUp.SetActive(false);
        Invoke("TriggerRandomPopup", Random.Range(10, 30));
    }

    private void TriggerRandomPopup()
    {
        if (!isPaused)
        {
            swapPopUp.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    public void ClosePopup()
    {
        swapPopUp.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;

        Invoke("TriggerRandomPopup", Random.Range(10, 30));
    }
}
