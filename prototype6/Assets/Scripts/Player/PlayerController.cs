using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GunController gunController;

    public void OnFire(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && gunController != null)
        {
            gunController.TryFire();
        }
    }
}
