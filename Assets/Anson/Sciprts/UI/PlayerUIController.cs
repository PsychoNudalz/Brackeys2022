using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField]
    private PlayerUIInteractOptionController playerUIInteractOptionController;

    public static PlayerUIController current;

    public PlayerUIInteractOptionController PlayerUIInteractOptionController => playerUIInteractOptionController;

    [Header("Retro Effects")]
    [SerializeField]
    private GameObject lowPixelRenderTexture;

    [SerializeField]
    private Material retroMaterial;
    private void Awake()
    {

        if (!current)
        {
            current = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        if (!playerUIInteractOptionController)
        {
            playerUIInteractOptionController = GetComponentInChildren<PlayerUIInteractOptionController>();
        }
        
        current.retroMaterial.SetFloat("_IsActive",1);

    }

    private void OnApplicationQuit()
    {
        current.retroMaterial.SetFloat("_IsActive",1);

    }

    public static void SetLowPixel(bool b)
    {
        current.lowPixelRenderTexture.SetActive(b);
    }

    public static void ToggleLowPixel()
    {
        current.lowPixelRenderTexture.SetActive(!current.lowPixelRenderTexture.activeSelf);

    }

    public static void ToggleRetroMaterial()
    {
        if (current.retroMaterial.GetFloat("_IsActive") >= 1f)
        {
            current.retroMaterial.SetFloat("_IsActive",0);
        }
        else
        {
            current.retroMaterial.SetFloat("_IsActive",1);

        }
    }

    public static void ResetLevel()
    {
        FindObjectOfType<GameManager>().RestartLevel();
    }

    public static void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public static void SkipLevel()
    {
        int temp = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        Debug.Log("Skip level to: "+temp);
        SceneManager.LoadScene(temp);
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
