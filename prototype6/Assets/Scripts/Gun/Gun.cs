using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Scriptable Objects/Gun")]
public class Gun : BaseGun
{
    // This is a concrete gun that can be assigned in the inspector
    // It inherits all magazine functionality from BaseGun
}
