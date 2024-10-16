using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameVariables gameVariables;

    public void PlayGame()
    {
        // Load the next scene in build order
        ResetParam();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    private void ResetParam()
    {
        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
        gameVariables.resourcesInfo.defenseMoney = 100;
        gameVariables.resourcesInfo.attackMoney = 100;
        gameVariables.resourcesInfo.defenseLife = 10;
    }
}
