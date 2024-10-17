using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameVariables gameVariables;

    public void PlayGame()
    {
        // Load the next scene in build order
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
