using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Player playerOne;
    [SerializeField] private Player playerTwo;

    private Transform GunContainerTransform(Player player) { return player.gunController.firePoint; }
    
    public void GiveGun(Player player, GameObject prefab)
    {
        
    }
}
