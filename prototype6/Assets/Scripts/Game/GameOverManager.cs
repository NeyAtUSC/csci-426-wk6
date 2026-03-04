using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;
    public TMP_Text winnerText;

    [Header("Timer")]
    public float roundDuration = 120f;
    public TMP_Text timerText;
    public Color normalTimerColor = Color.white;
    public Color warningTimerColor = new Color(0.95f, 0.25f, 0.25f);
    public float warningThreshold = 30f;

    [Header("Players")]
    public PlayerHealth player1Health;
    public PlayerHealth player2Health;

    private float _timeRemaining;
    private bool _gameIsOver = false;

    private void Awake()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void Start()
    {
        _timeRemaining = roundDuration;
    }

    private void Update()
    {
        if (_gameIsOver) return;

        _timeRemaining -= Time.deltaTime;

        // Update timer display
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(_timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(_timeRemaining % 60f);
            timerText.text = $"{minutes}:{seconds:00}";
            timerText.color = _timeRemaining <= warningThreshold ? warningTimerColor : normalTimerColor;
        }

        if (_timeRemaining <= 0f)
        {
            _timeRemaining = 0f;
            ShowDraw();
        }
    }

    // Called by PlayerHealth.Die()
    public void OnPlayerDied(int playerNumber)
    {
        if (_gameIsOver) return;
        ShowGameOver(playerNumber);
    }

    private void ShowGameOver(int losingPlayer)
    {
        _gameIsOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        int winningPlayer = losingPlayer == 1 ? 2 : 1;

        if (gameOverText != null)
            gameOverText.text = $"Player {losingPlayer} Lost!";

        if (winnerText != null)
            winnerText.text = $"Player {winningPlayer} Wins!";

        Debug.Log($"Game Over! Player {losingPlayer} lost. Player {winningPlayer} wins!");

        Time.timeScale = 0f;
    }

    private void ShowDraw()
    {
        _gameIsOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (gameOverText != null)
            gameOverText.text = "Time's Up!";

        if (winnerText != null)
        {
            float p1HP = player1Health != null ? player1Health.currentHealth : 0;
            float p2HP = player2Health != null ? player2Health.currentHealth : 0;

            if (p1HP > p2HP)
                winnerText.text = "Player 1 Wins! (More HP)";
            else if (p2HP > p1HP)
                winnerText.text = "Player 2 Wins! (More HP)";
            else
                winnerText.text = "Draw!";
        }

        Debug.Log("Game Over! Time's up.");
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScreen");
    }
}