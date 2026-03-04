using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;
    public int currentHealth;
    public int playerNumber = 1;

    [Header("HUD")]
    public HealthHUD healthHUD;

    [Header("Events")]
    public UnityEvent<int> onHealthChanged;
    public UnityEvent<int> onPlayerDied;

    private bool _isDead = false;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthHUD?.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (_isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);
        
        Debug.Log($"[PlayerHealth] Player {playerNumber} took {damage} damage — {currentHealth}/{maxHealth}");
        
        healthHUD?.UpdateHealth(currentHealth, maxHealth);
        onHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        if (_isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Min(maxHealth, currentHealth);
        
        healthHUD?.UpdateHealth(currentHealth, maxHealth);
        onHealthChanged?.Invoke(currentHealth);
    }

    private void Die()
    {
        _isDead = true;
        Debug.Log($"[PlayerHealth] Player {playerNumber} died!");
        
        onPlayerDied?.Invoke(playerNumber);

        GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
        if (gameOverManager != null)
        {
            Debug.Log($"[PlayerHealth] Notifying GameOverManager");
            gameOverManager.OnPlayerDied(playerNumber);
        }
        else
        {
            Debug.LogWarning("[PlayerHealth] GameOverManager not found!");
        }
    }

    public bool IsDead() => _isDead;
    public float GetHealthPercentage() => (float)currentHealth / maxHealth;
}