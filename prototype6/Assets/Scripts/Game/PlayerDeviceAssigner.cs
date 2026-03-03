using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDeviceAssigner : MonoBehaviour
{
    public PlayerInput playerOne;
    public PlayerInput playerTwo;

    private void Start()
    {
        if (Gamepad.all.Count >= 2)
        {
            playerOne.SwitchCurrentControlScheme("Gamepad", Gamepad.all[0]);
            playerTwo.SwitchCurrentControlScheme("Gamepad", Gamepad.all[1]);
        }
        else
        {
            Debug.LogWarning("Less than 2 gamepads connected!");
        }
    }
}