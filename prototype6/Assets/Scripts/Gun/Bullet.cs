using UnityEngine;

public abstract class Bullet : ScriptableObject
{
    public float damage;
    public abstract void Fire(Transform firePoint);
}