using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 5.0f;
    public float drag = 10f;

    private Vector2 _moveInput;

    private void Awake()
    {
        rb.linearDamping = drag;
        rb.useGravity = false;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(_moveInput.x, 0f, _moveInput.y);
        rb.AddForce(moveDirection * moveSpeed, ForceMode.VelocityChange);

        // Clamp XZ velocity
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 clamped = flatVelocity.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(clamped.x, rb.linearVelocity.y, clamped.z);
        }
    }
}