using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    public void LoadPauseScreen()
    {
        pauseScreen.SetActive(true);
        
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
