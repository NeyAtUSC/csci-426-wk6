using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    public int playerNumber = 1; // 1 or 2 for Player 1/Player 2

    [Header("Events")]
    public UnityEvent<float> onHealthChanged;
    public UnityEvent<int> onPlayerDied; // passes player number

    private bool isDead = false;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(0f, currentHealth);
        
        Debug.Log($"Player {playerNumber} took {damage} damage. Health: {currentHealth}/{maxHealth}");
        
        onHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0f && !isDead)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Min(maxHealth, currentHealth);
        
        onHealthChanged?.Invoke(currentHealth);
    }

    private void Die()
    {
        isDead = true;
        Debug.Log($"Player {playerNumber} has died!");
        
        onPlayerDied?.Invoke(playerNumber);

        // Notify GameOverManager
        GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager != null)
        {
            gameOverManager.OnPlayerDied(playerNumber);
        }
    }

    public bool IsDead() => isDead;
    public float GetHealthPercentage() => currentHealth / maxHealth;
}
