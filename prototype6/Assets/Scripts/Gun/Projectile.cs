using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Bullets/ProjectileBullet")]
public class Projectile : Bullet
{
    public GameObject prefab;
    public float speed = 25f; // Default high speed for projectile
    public GameObject muzzleFlashEffect; // Particle effect at gun barrel
    public GameObject bulletTrailEffect; // Trail effect attached to bullet

    public override void Fire(Transform firePoint)
    {
        if (prefab == null) return;
        
        // Spawn muzzle flash effect at gun barrel
        if (muzzleFlashEffect != null)
        {
            GameObject flash = Instantiate(muzzleFlashEffect, firePoint.position, firePoint.rotation);
            Destroy(flash, 2f); // Auto-destroy after 2 seconds
        }
        
        // Spawn bullet
        GameObject proj = Instantiate(prefab, firePoint.position, firePoint.rotation);
        
        // Attach trail effect to bullet
        if (bulletTrailEffect != null)
        {
            GameObject trail = Instantiate(bulletTrailEffect, proj.transform);
            trail.transform.localPosition = Vector3.zero;
        }
        
        // Use 2D physics for 2D game
        if (proj.TryGetComponent<Rigidbody2D>(out var rb2d))
        {
            rb2d.gravityScale = 0f; // Disable gravity for straight bullet travel
            rb2d.linearDamping = 0f; // No drag
            
            // In 2D, use right direction (assumes gun sprite faces right)
            Vector2 direction = firePoint.right;
            rb2d.linearVelocity = direction * speed;
            
            Debug.Log($"Bullet fired at speed {speed} in direction {direction}");
        }
    }
}
