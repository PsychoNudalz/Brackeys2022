using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform archerSpawnpoint;
    [SerializeField] private Transform mageSpawnpoint;

    [SerializeField] private GameObject mage;
    [SerializeField] private GameObject archer;
    [SerializeField] private CanvasGroup canvas;

    public int characterAmount;
    public int characterDeaths;

    public static GameManager current;

    private void Awake()
    {
        if (!current)
        {
            current = this;
        }
        else
        {
            Destroy(current.gameObject);
            current = this;
        }
    }

    private void startLevel(bool archerSpawned, bool mageSpawned, int charAmt)
    {
        LeanTween.alphaCanvas(canvas, 0, 1).setEaseInOutBack();
        characterAmount = charAmt;
        if (archerSpawned)
        {
            archer.transform.position = archerSpawnpoint.transform.position;
            archer.transform.rotation = archerSpawnpoint.transform.rotation;
            archer.SetActive(true);
            if (mageSpawned)
            {
                mage.transform.position = mageSpawnpoint.transform.position;
                mage.transform.rotation = mageSpawnpoint.transform.rotation;
                mage.SetActive(true);
            }
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 1:
                startLevel(false, false, 1);
                break;
        }
    }

    public void checkForGameOver() // force restart level if all characters are dead
    {
        characterDeaths++;
        if (characterDeaths == characterAmount)
            StartCoroutine(resetLevel());
    }

    public void loadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            Debug.Log("that was the final level");
    }

    public IEnumerator resetLevel()
    {
        yield return new WaitForSeconds(2f);
        LeanTween.alphaCanvas(canvas, 1, 1).setEaseInOutBack();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
