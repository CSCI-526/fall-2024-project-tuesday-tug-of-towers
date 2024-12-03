using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Required for TextMeshPro

public class AttackerWin : MonoBehaviour
{
    // Reference to the TextMeshPro text object for the title
    public TMP_Text titleText;

    private void Start()
    {
        // Update the text to show the attacker's name in all caps
        if (titleText != null)
        {
            if (!string.IsNullOrEmpty(GameVariables.attackerName))
            {
                titleText.text = GameVariables.attackerName.ToUpper() + " WON!!!";
            }
            else
            {
                titleText.text = "ATTACKER WON!!!"; // Fallback in case name is not set
            }
        }
        else
        {
            Debug.LogWarning("Title Text reference is not assigned in the Inspector.");
        }
    }

    // This function is called when the "Back to Main Menu" button is clicked
    public void BackToMainMenu()
    {
        // Assuming the MainMenu scene is at index 0 in Build Settings
        SceneManager.LoadScene("Menu");
    }
}