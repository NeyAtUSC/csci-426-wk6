using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public BaseGun gunData;
    public Transform firePoint;

    public void TryFire()
    {
        Debug.Log($"TryFire called - gunData: {gunData != null}, firePoint: {firePoint != null}");
        if (gunData == null || firePoint == null) return;
        if (gunData.IsReloading) return;

        Bullet bullet = gunData.Fire();
        Debug.Log($"Bullet from magazine: {bullet != null}");
        bullet?.Fire(firePoint);

        if (gunData.IsEmpty())
            StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        gunData.SetReloading(true);
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.Reload(gunData.capacity);
        gunData.SetReloading(false);
        Debug.Log("Reloaded!");
    }

    public void OnLoadGun()
    {
        if (gunData == null || gunData.IsReloading) return;
        StartCoroutine(Reload());
    }

    public void OnShoot() => TryFire();
}