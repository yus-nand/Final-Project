using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


#if UNITY_EDITOR
using UnityEditor;
#endif
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetupButtons();
    }
    public void LoadMain()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif    
    }
    private void SetupButtons()
    {
        Button playButton = GameObject.Find("Play")?.GetComponent<Button>();
        Button exitButton = GameObject.Find("Exit")?.GetComponent<Button>();

        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            playButton.onClick.AddListener(LoadMain);
            exitButton.onClick.AddListener(Exit);
        }
    }
}

