using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GunController gunController;
    public int playerNumber = 1; // 1 or 2 for Player 1/Player 2

    public void OnFire(InputAction.CallbackContext ctx)
    {
        Debug.Log($"OnFire called - performed: {ctx.performed}, gunController: {gunController != null}");
        if (ctx.performed && gunController != null)
        {
            gunController.SetOwner(playerNumber);
            gunController.TryFire();
        }
    }
}
