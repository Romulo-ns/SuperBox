using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    [Header("Menu Scene Name")]
    public string menuSceneName = "MainMenu"; // exact scene name of the main menu

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Player touched the spike → go back to menu
            SceneManager.LoadScene(menuSceneName);
        }
    }
}
