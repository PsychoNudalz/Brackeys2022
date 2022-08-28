using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform archerSpawnpoint;

    [SerializeField]
    private Transform mageSpawnpoint;

    [SerializeField]
    private GameObject mage;

    [SerializeField]
    private GameObject archer;

    [SerializeField]
    private CanvasGroup canvas;

    public int characterAmount;
    public int characterDeaths;

    public static GameManager current;

    private Coroutine resetLevelCoroutine;

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

    private void Start()
    {
        archer = CharacterManager.GetArcher().GameObject();
        mage = CharacterManager.GetMage().GameObject();
        canvas.gameObject.SetActive(true);


        //Override so that it takes all 3 characters on other scenes
    }

    private void startLevel(bool archerSpawned=false, bool mageSpawned=false, int charAmt=3)
    {
        if (canvas)
        {
            LeanTween.alphaCanvas(canvas, 0, 1).setEaseInOutBack();
        }
        characterAmount = charAmt;
        if (archerSpawned)
        {
            CharacterManager.GetArcher().SetActive(true);
        }
        if (mageSpawned)
        {
            CharacterManager.GetMage().SetActive(true);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        canvas.gameObject.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        canvas.gameObject.SetActive(false);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 0:
                //Main Menu
                break;
            case 1:
                startLevel(false, false, 1);
                break;
            case 2:
                startLevel(false, false, 1);
                break;
            case 3:
                startLevel(true, false, 2);
                break;
            case 4:
                startLevel(true, false, 2);
                break;
            default:
                startLevel(true, true, 3);
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
        {
            Debug.Log("that was the final level");
            SceneManager.LoadScene(0);
        }
    }

    public IEnumerator resetLevel()
    {
        yield return new WaitForSeconds(2f);
        LeanTween.alphaCanvas(canvas, 1, 1).setEaseInOutBack();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator resetLevel_now()
    {
        LeanTween.alphaCanvas(canvas, 1, 1).setEaseInOutBack();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartLevel()
    {
        if (resetLevelCoroutine == null)
        {
            resetLevelCoroutine = StartCoroutine(resetLevel_now());
        }
    }
}