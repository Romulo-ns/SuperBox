using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Called when the player clicks the "Start" button
    public void StartGame()
    {
        // Load the main game scene
        SceneManager.LoadScene("GameScene");
    }

    // Called when the player clicks the "Settings" button
    public void OpenSettings()
    {
        // You can load a settings scene or enable a settings panel here
        Debug.Log("Opening Settings...");
    }

    // Called when the player clicks the "Exit" button
    public void QuitGame()
    {
        Debug.Log("Exiting the game...");
        Application.Quit(); // Works only in a build, not in the editor
    }
}
