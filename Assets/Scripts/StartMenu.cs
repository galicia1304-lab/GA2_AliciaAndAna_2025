using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void CallQuit()
    {
        Application.Quit();
    }

    public void CallLoad(int level)
    {
        SceneManager.LoadScene(level);
    }

}
