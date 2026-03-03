using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileController : MonoBehaviour
{
    public float lifetime = 5f; // Auto-destroy after 5 seconds
    private Rigidbody2D rb;
    
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
        Debug.Log($"Bullet velocity: {rb.linearVelocity}, Position: {transform.position}");
    }
}

