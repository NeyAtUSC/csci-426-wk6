using UnityEngine;
using UnityEngine.InputSystem;

public class AimReticle : MonoBehaviour
{
    [Header("Settings")]
    public float reticleDistance = 2f;
    public Transform playerTransform;
    public Transform gunTransform;

    private Vector2 _aimInput;

    public void OnAim(InputAction.CallbackContext ctx)
    {
        _aimInput = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (_aimInput.magnitude < 0.1f) return;

        Vector3 aimDirection = new Vector3(_aimInput.x, 0f, _aimInput.y).normalized;

        // Position reticle
        transform.position = playerTransform.position + aimDirection * reticleDistance;

        // Rotate gun to face aim direction
        if (gunTransform != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(aimDirection, Vector3.up);
            gunTransform.rotation = targetRotation;
        }
    }
    
    
}