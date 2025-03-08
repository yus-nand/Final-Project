using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    // public static MainManager Instance;
    // public GameObject playerInstance;
    [SerializeField] private NavMeshSurface navMeshSurface;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private GameObject ColorButtons;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject playerPF;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private TextMeshProUGUI startNextWaveText;
    private Image crossHairImage;
    private Player player;
    private bool isWaveComplete = false;
    private bool canStartNextWave = false;
    private bool gameOver = false;
    private int waveNumber = 0;
    private int enemiesToSpawn = 3;
    // private int maxRange = 0;
    // private void Awake()
    // {
    //     playerInstance = Instantiate(playerPF, new Vector3(3, 17, -34), playerPF.transform.rotation);
    // }
    private void Start()
    {
        Time.timeScale = 1f;
        crossHairImage = GameObject.Find("CrossHair").GetComponent<Image>();
        startNextWaveText = GameObject.Find("StartNextWaveText").GetComponent<TextMeshProUGUI>();
        startNextWaveText.text = "Press Q to start next wave";
        startNextWaveText.enabled = false;
        player = GameObject.Find("Player").GetComponent<Player>();
        StartCoroutine(SpawnEnemyWave());
        waveNumber++;
    }
    private void Update()
    {
        // navMeshSurface.BuildNavMesh();
        // if(GameObject.FindGameObjectsWithTag("Enemy").Length == 3)
        // {
        //     StopAllCoroutines();
        // }
        if(Input.GetKeyDown(KeyCode.P))   
        {
            LoadPauseScreen();
        }
        if(Input.GetKeyDown(KeyCode.U))
            Time.timeScale = 1f;
        Application.targetFrameRate = 120;
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !isWaveComplete)
        {
            isWaveComplete = true;
            Debug.Log("All Enemies killed");
            waveNumber++;
            Debug.Log(waveNumber);
            startNextWaveText.enabled = true;
            if(waveNumber == 3)
            {
                Debug.Log("Max Range increased");
            }
            canStartNextWave = true;
        }
        if(Input.GetKeyDown(KeyCode.Q) && canStartNextWave)
        {
            enemiesToSpawn++;
            startNextWaveText.enabled = false;
            canStartNextWave = false;
            isWaveComplete = false;
            StartCoroutine(SpawnEnemyWave());
        }
        gameOver = isGameOver();
        if(gameOver)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            gameOverScreen.SetActive(true);
            crossHairImage.enabled = false;
        }
    }
    private bool isGameOver()
    {
        if(player.playerHealth <= 0)
            return true;
        else
            return false;
    }
    IEnumerator SpawnEnemyWave()
    {
        int enemyIndex = 0;
        // bool waveNumberIncreased = false;
        // if(waveNumber == 3 && !waveNumberIncreased)
        // {
        //     enemyIndex++;
        //     Debug.Log("Max Range increased");
        //     enemiesToSpawn = 1;
        //     waveNumberIncreased = true;
        // }
        // int enemyIndex = Random.Range(0, maxRange);
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemies[enemyIndex], new Vector3(-11, 15, -31), enemies[enemyIndex].transform.rotation);
            yield return new WaitForSeconds(1f);
        }    
        // while(true && GameObject.FindGameObjectsWithTag("Enemy").Length != enemiesToSpawn)
        // {
        //     int enemyIndex = Random.Range(0, maxRange);
        //     // for(int i = 0; i < enemiesToSpawn; i++)
        //     // {
        //     //     Instantiate(enemies[enemyIndex], new Vector3(-11, 15, -31), enemies[enemyIndex].transform.rotation);
        //     //     yield return new WaitForSeconds(1f);
        //     // }    
        //     Instantiate(enemies[enemyIndex], new Vector3(-11, 15, -31), enemies[enemyIndex].transform.rotation);
        //     yield return new WaitForSeconds(1f);
        //     SpawnEnemyWave();
        // }
    }
    public void LoadPauseScreen()
    {
        pauseScreen.SetActive(true);
        crossHairImage.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        pauseScreen.SetActive(false);
        crossHairImage.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Options()
    {
        optionsScreen.SetActive(true);
        pauseScreen.SetActive(false);
    }
    public void Back()
    {
        pauseScreen.SetActive(true);
        ColorButtons.SetActive(false);
        optionsScreen.SetActive(false);
    }
    public void Red()
    {
        crossHairImage.color = Color.red;
    }
    public void Green()
    {
        crossHairImage.color = Color.green;
    }
    public void Blue()
    {
        crossHairImage.color = Color.blue;
    }
    public void Yellow()
    {
        crossHairImage.color = Color.yellow;
    }
    public void ChangeCrosshairColor()
    {
        ColorButtons.SetActive(true);
    }
    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    
}
