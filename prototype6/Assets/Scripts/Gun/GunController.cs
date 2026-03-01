using UnityEngine;

public class GunController : MonoBehaviour
{
    public BaseGun gunData;
    public Transform firePoint;

    public void TryFire()
    {
        Bullet bullet = gunData.Fire();
        bullet?.Fire(firePoint);
    }
}
