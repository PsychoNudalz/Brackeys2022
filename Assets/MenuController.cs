using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] CanvasGroup fade;

    private void Start()
    {
        LeanTween.alphaCanvas(fade, 0, 1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void loadLevel(int sceneIndex)
    {
        StartCoroutine(loadIndex(sceneIndex));
    }

    public IEnumerator loadIndex(int index)
    {
        LeanTween.alphaCanvas(fade, 1, 1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
    }
}
