using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileController : MonoBehaviour
{
    public float lifetime = 5f;
    public int damage = 1;
    private Rigidbody _rb;
    private int _ownerPlayerNumber = 0;
    private bool _hasHit = false;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.linearDamping = 0f;
        _rb.angularDamping = 0f;
        _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetOwner(int playerNumber) => _ownerPlayerNumber = playerNumber;
    public void SetDamage(int damageAmount) => damage = damageAmount;

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasHit) return;
        _hasHit = true;

        Debug.Log($"[Projectile] OnCollisionEnter hit: {collision.gameObject.name}");
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null && playerHealth.playerNumber != _ownerPlayerNumber)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log($"[Projectile] Hit Player {playerHealth.playerNumber} for {damage} damage!");
        }
        Destroy(gameObject);
    }
}