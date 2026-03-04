using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GunController gunController;
    public ChamberHUD hud;
    public AimReticle aimReticle;

    public int playerNumber = 1; // 1 or 2 for Player 1/Player 2

    
    private static readonly Color[] PlayerColors = new Color[]
    {
        new Color(0.95f, 0.25f, 0.25f), // P1 red
        new Color(0.25f, 0.55f, 0.95f), // P2 blue
    };
    private void Awake()
    {
        Color color = PlayerColors[playerNumber - 1];
        if (hud != null)
            hud.playerColor = color;
    }
    public void OnAim(InputAction.CallbackContext ctx)
    {
        if (aimReticle != null)
            aimReticle.OnAim(ctx);
    }
    public void OnFire(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (gunController == null) return;
        
        gunController.SetOwner(playerNumber);
        gunController.TryFire();
    }
}
