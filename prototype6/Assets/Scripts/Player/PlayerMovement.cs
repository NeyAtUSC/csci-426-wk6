using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float moveSpeed = 5.0f;
    
    private Vector2 _moveInput;
    private void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(_moveInput.x, 0f, _moveInput.y);
        rb.AddForce(moveDirection * moveSpeed, ForceMode.Acceleration);
    }
}
