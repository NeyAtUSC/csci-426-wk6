using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileController : MonoBehaviour
{
    public float lifetime = 5f; // Auto-destroy after 5 seconds
    public float damage = 10f; // Damage dealt on hit
    private Rigidbody2D rb;
    private int ownerPlayerNumber = 0; // Track who fired this bullet
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Ensure proper 2D physics setup
        rb.gravityScale = 0f;
        rb.linearDamping = 0f;
        rb.angularDamping = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        
        Debug.Log($"Bullet spawned - Gravity: {rb.gravityScale}, Velocity will be set by Projectile.Fire()");
    }
    
    void Start()
    {
        // Destroy bullet after lifetime expires
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Track bullet position (disabled to reduce console spam)
        // Debug.Log($"Bullet velocity: {rb.linearVelocity}, Position: {transform.position}");
    }

    public void SetOwner(int playerNumber)
    {
        ownerPlayerNumber = playerNumber;
    }

    public void SetDamage(float damageAmount)
    {
        damage = damageAmount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if we hit a player
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            // Don't let players damage themselves
            if (playerHealth.playerNumber != ownerPlayerNumber)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log($"Bullet hit Player {playerHealth.playerNumber} for {damage} damage!");
            }
        }

        // Destroy bullet on any collision
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if we hit a player (for trigger colliders)
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            // Don't let players damage themselves
            if (playerHealth.playerNumber != ownerPlayerNumber)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log($"Bullet hit Player {playerHealth.playerNumber} for {damage} damage!");
            }
        }

        // Destroy bullet on any collision
        Destroy(gameObject);
    }
}

