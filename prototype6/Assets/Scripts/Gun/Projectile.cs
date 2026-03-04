using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Bullets/ProjectileBullet")]
public class Projectile : Bullet
{
    public GameObject prefab;
    public float speed = 500f;
    public GameObject muzzleFlashEffect;
    public GameObject bulletTrailEffect;

    public override void Fire(Transform firePoint)
    {
        if (prefab == null) return;

        if (muzzleFlashEffect != null)
        {
            GameObject flash = Instantiate(muzzleFlashEffect, firePoint.position, firePoint.rotation);
            Destroy(flash, 2f);
        }

        GameObject proj = Instantiate(prefab, firePoint.position, firePoint.rotation);

        if (proj.TryGetComponent<ProjectileController>(out var projController))
        {
            projController.SetDamage(damage);
            projController.SetOwner(ownerPlayerNumber);
    
            // Ignore collision with the owner player
            Collider projCollider = proj.GetComponent<Collider>();
            Collider ownerCollider = firePoint.GetComponentInParent<Collider>();
            if (projCollider != null && ownerCollider != null)
                Physics.IgnoreCollision(projCollider, ownerCollider);
        }

        if (bulletTrailEffect != null)
        {
            GameObject trail = Instantiate(bulletTrailEffect, proj.transform);
            trail.transform.localPosition = Vector3.zero;
            trail.transform.localRotation = Quaternion.identity;
        }

        if (proj.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.useGravity = false;
            rb.linearDamping = 0f;
            // Use forward direction in 3D — firePoint should face the aim direction
            rb.linearVelocity = firePoint.forward * speed;
            Debug.Log($"Bullet fired at speed {speed} in direction {firePoint.forward}");
        }
    }
}