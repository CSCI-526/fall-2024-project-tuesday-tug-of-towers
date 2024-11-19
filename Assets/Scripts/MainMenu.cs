using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameVariables gameVariables;

    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }
    
    public void PlayTutorial()
    {
        SceneManager.LoadScene(1);
    }
}
