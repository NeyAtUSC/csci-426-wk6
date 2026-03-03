using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Debug.Log($"Loading scene: {sceneName}");
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void LoadMainGame()
    {
        LoadScene("TestScene");
    }

    public void Quit()
    {
        Debug.Log("Quitting game");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
