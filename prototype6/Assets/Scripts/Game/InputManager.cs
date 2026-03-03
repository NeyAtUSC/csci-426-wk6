using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R pressed - Loading StartScreen");
            UnityEngine.SceneManagement.SceneManager.LoadScene("StartScreen");
        }
    }
}
