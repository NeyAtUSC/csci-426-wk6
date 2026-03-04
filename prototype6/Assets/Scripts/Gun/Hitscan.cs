using UnityEngine;

[CreateAssetMenu(fileName = "Hitscan", menuName = "Bullets/HitscanBullet")]
public class Hitscan : Bullet
{
    public float range;

    public override void Fire(Transform firePoint)
    {
        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, range))
        {
            Debug.Log($"Hit {hit.collider.name} for {damage} damage");
            
            PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null && playerHealth.playerNumber != ownerPlayerNumber)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
