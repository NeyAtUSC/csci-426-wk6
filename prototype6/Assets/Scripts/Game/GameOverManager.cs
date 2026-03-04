using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;
    public TMP_Text winnerText;

    private bool gameIsOver = false;

    private void Awake()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void OnPlayerDied(int playerNumber)
    {
        if (gameIsOver) return;

        gameIsOver = true;
        ShowGameOver(playerNumber);
    }

    private void ShowGameOver(int losingPlayer)
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        int winningPlayer = losingPlayer == 1 ? 2 : 1;

        if (gameOverText != null)
            gameOverText.text = $"Player {losingPlayer} Lost!";

        if (winnerText != null)
            winnerText.text = $"Player {winningPlayer} Wins!";

        Debug.Log($"Game Over! Player {losingPlayer} lost. Player {winningPlayer} wins!");

        // Pause game or disable player controls
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScreen");
    }
}
