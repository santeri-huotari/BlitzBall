using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float time;
    private int playerHealth;
    private int playerMaxHealth;

    private Text timerText;
    private Text healthText;
    private GameObject player;
    private GameObject victoryMenu;
    private GameObject pauseMenu;
    private GameObject gameoverMenu;

    private Scene currentScene;

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// Gets called when a scene is loaded. Does Scene related things like disabling UI elements.
    /// </summary>
    /// <param name="scene">Scene that's loaded</param>
    /// <param name="loadSceneMode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        currentScene = SceneManager.GetActiveScene();

        if (scene.name == "Initialize")
        {
            SceneManager.LoadScene("MainMenu");
        }
        // If currently in levels do this (not in menu or Intialize)
        else if (scene.buildIndex > 1)
        {
            timerText = GameObject.Find("TimerText").GetComponent<Text>();
            healthText = GameObject.Find("HealthText").GetComponent<Text>();
            player = GameObject.FindGameObjectWithTag("Player");
            victoryMenu = GameObject.Find("VictoryPanel");
            pauseMenu = GameObject.Find("PausePanel");
            gameoverMenu = GameObject.Find("GameOverPanel");

            victoryMenu.SetActive(false);
            pauseMenu.SetActive(false);
            gameoverMenu.SetActive(false);

            time = 0f;
            Time.timeScale = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScene.buildIndex > 1)
        {
            UpdateUI();
        }

        if (Input.GetButtonDown("Cancel") && currentScene.buildIndex > 1 && Time.timeScale != 0)
        {
            TogglePause();
        }
    }

    private void UpdateUI()
    {
        playerHealth = player.GetComponent<PlayerController>().health;
        playerMaxHealth = player.GetComponent<PlayerController>().maxHealth;

        time += Time.deltaTime;
        timerText.text = "Time: " + time;
        healthText.text = "Health: " + playerHealth + "/" + playerMaxHealth;
    }

    public void Victory()
    {
        Time.timeScale = 0f;
        victoryMenu.SetActive(true);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void TogglePause()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameoverMenu.SetActive(true);
    }
}
