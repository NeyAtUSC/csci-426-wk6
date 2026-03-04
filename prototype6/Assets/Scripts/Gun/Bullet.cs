using UnityEngine;

public abstract class Bullet : ScriptableObject
{
    public float damage;
    protected int ownerPlayerNumber = 0;
    
    public void SetOwner(int playerNumber)
    {
        ownerPlayerNumber = playerNumber;
    }
    
    public abstract void Fire(Transform firePoint);
}