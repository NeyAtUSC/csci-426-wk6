using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Bullets/ProjectileBullet")]
public class Projectile : Bullet
{
    public GameObject prefab;
    public float speed;

    public override void Fire(Transform firePoint)
    {
        if (prefab == null) return;
        GameObject proj = Instantiate(prefab, firePoint.position, firePoint.rotation);
        if (proj.TryGetComponent<Rigidbody>(out var rb))
            rb.linearVelocity = firePoint.forward * speed;
    }
}
