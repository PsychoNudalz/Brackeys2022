using UnityEngine.SceneManagement;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void quitThisShite()
    {
        Application.Quit();
    }

    public void loadMain()
    {
        SceneManager.LoadScene(0);
    }
}
