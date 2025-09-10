using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("Menu Scene Name")]
    public string menuSceneName = "MainMenu";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object is the player
        if (collision.CompareTag("Player"))
        {
            // Reload menu scene
            SceneManager.LoadScene(menuSceneName);
        }
    }
}
