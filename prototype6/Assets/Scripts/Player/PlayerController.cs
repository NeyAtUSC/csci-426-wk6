using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GunController gunController;

    public void OnFire(InputAction.CallbackContext ctx)
    {
        Debug.Log($"OnFire called - performed: {ctx.performed}, gunController: {gunController != null}");
        if (ctx.performed && gunController != null)
        {
            gunController.TryFire();
        }
    }
}
