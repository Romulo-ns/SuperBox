using UnityEngine;
using TMPro; // Importa o TextMeshPro

public class PlayerScore : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText; // Referência para o texto na UI

    private GameObject lastPlatform = null; // guarda a plataforma anterior

    private void Start()
    {
        // Atualiza o texto inicial
        UpdateScoreUI();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            GameObject currentPlatform = collision.gameObject;

            if (lastPlatform == null)
            {
                lastPlatform = currentPlatform;
            }
            else
            {
                if (currentPlatform != lastPlatform)
                {
                    score += 1;
                }
                else
                {
                    score -= 1;
                    if (score < 0) score = 0;
                }

                lastPlatform = currentPlatform;
                UpdateScoreUI(); // sempre atualiza o texto
            }
        }
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
}
