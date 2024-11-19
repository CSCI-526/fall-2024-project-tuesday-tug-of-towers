using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackerWin : MonoBehaviour
{
    // This function is called when the "Back to Main Menu" button is clicked
    public void BackToMainMenu()
    {
        // Assuming the MainMenu scene is at index 0 in Build Settings
        SceneManager.LoadScene(0);
    }
}
