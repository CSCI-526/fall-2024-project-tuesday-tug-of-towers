using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    // UI Elements
    public GameObject pauseMenu; // Reference to the pause menu panel
    public Button restartButton; // Button to restart the game
    public Button menuButton; // Button to go to the main menu
    public Button closeButton; // Button to close the pause menu

    private bool isGamePaused = false; // Flag to check if the game is paused

    private void Start()
    {
        // Add listeners to the buttons
        restartButton.onClick.AddListener(RestartGame);
        menuButton.onClick.AddListener(GoToMainMenu);
        closeButton.onClick.AddListener(ClosePauseMenu);
    }

    private void Update()
    {
        // Toggle pause menu with the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    // Called when the pause button is clicked
    public void TogglePauseMenu()
    {
        isGamePaused = !isGamePaused; // Toggle pause state
        pauseMenu.SetActive(isGamePaused); // Show or hide pause menu

        // Pause or resume the game
        Time.timeScale = isGamePaused ? 0 : 1;
    }

    // Restart the current game scene
    private void RestartGame()
    {
        Time.timeScale = 1; // Resume time before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    // Go back to the main menu
    private void GoToMainMenu()
    {
        Time.timeScale = 1; // Resume time before switching scenes
        SceneManager.LoadScene("Menu"); // Load the main menu scene
    }

    // Close the pause menu and resume the game
    private void ClosePauseMenu()
    {
        isGamePaused = false; // Unpause the game
        pauseMenu.SetActive(false); // Hide the pause menu
        Time.timeScale = 1; // Resume time
    }
}
