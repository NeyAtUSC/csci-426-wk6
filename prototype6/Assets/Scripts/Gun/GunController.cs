using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public BaseGun gunData;
    public Transform firePoint;
    public ChamberHUD hud;
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip gunFireSound;
    public AudioClip gunDryFireSound;
    public AudioClip gunReloadSound;
    
    private int _ownerPlayerNumber = 0;

    private void Awake()
    {
        gunData = Instantiate(gunData);
    }
    
    public void TryFire()
    {
        if (gunData == null || firePoint == null) return;
        if (gunData.IsReloading) return;
        if (gunData.IsOnCooldown()) return;

        Bullet bullet = gunData.Fire();

        if (bullet != null)
        {
            if (audioSource != null && gunFireSound != null)
                audioSource.PlayOneShot(gunFireSound);

            bullet.SetOwner(_ownerPlayerNumber);
            bullet.Fire(firePoint);
        }
        else
        {
            if (audioSource != null && gunDryFireSound != null)
                audioSource.PlayOneShot(gunDryFireSound);
        }

        Debug.Log($"[GunController] About to call hud.OnChamberFired — hud is null: {hud == null}");
        hud?.OnChamberFired(bullet != null);

        if (gunData.IsCylinderSpent())
            StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        gunData.SetReloading(true);
        hud?.OnReloadStart(gunData.reloadTime);
    
        if (audioSource != null && gunReloadSound != null)
            audioSource.PlayOneShot(gunReloadSound);
    
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.Reload();
        gunData.SetReloading(false);
    }

    public void OnLoadGun()
    {
        if (gunData == null || gunData.IsReloading) return;
        StartCoroutine(Reload());
    }

    public void OnShoot() => TryFire();

    public void SetOwner(int playerNumber)
    {
        _ownerPlayerNumber = playerNumber;
    }
}