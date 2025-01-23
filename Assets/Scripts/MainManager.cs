using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    // public static MainManager Instance;
    // public GameObject playerInstance;
    [SerializeField] private NavMeshSurface navMeshSurface;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject playerPF;
    [SerializeField] private GameObject[] enemies;
    // private void Awake()
    // {
    //     playerInstance = Instantiate(playerPF, new Vector3(3, 17, -34), playerPF.transform.rotation);
    // }
    private void Start()
    {
        Instantiate(enemies[0], new Vector3(0, 17, -34), enemies[0].transform.rotation);
    }
    private void Update()
    {
        // navMeshSurface.BuildNavMesh();
    }
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
