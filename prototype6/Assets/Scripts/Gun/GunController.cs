using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public BaseGun gunData;
    public Transform firePoint;
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip gunFireSound;
    
    private int ownerPlayerNumber = 0;

    public void TryFire()
    {
        Debug.Log($"TryFire called - gunData: {gunData != null}, firePoint: {firePoint != null}");
        if (gunData == null || firePoint == null) return;
        if (gunData.IsReloading) return;

        Bullet bullet = gunData.Fire();
        Debug.Log($"Bullet from magazine: {bullet != null}");
        
        if (bullet != null)
        {
            // Play gun fire sound
            if (audioSource != null && gunFireSound != null)
            {
                audioSource.PlayOneShot(gunFireSound);
            }
            
            bullet.SetOwner(ownerPlayerNumber);
            bullet.Fire(firePoint);
        }

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

    public void SetOwner(int playerNumber)
    {
        ownerPlayerNumber = playerNumber;
    }
}