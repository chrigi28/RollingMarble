using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } 

    private EGameState gameState = EGameState.Paused;

    public GameObject GameOverPanel;
    public GameObject PausePanel;
    private GameObject currentLevel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        this.DisableMenus();
    }

    void Start()
    {
        this.DisableMenus();
    }

    void OnEnable()
    {
        
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        var timerScript = gameObject.GetComponentInChildren<TimerScript>();
        this.DisableMenus();
        gameState = EGameState.Countdown;
        Time.timeScale = 1;
        timerScript.Restart();

    }

    public void SelectLevel()
    {
        //load LevelSelectionScene
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        gameState = EGameState.Paused;
        this.GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public bool IsRunning()
    {
        return gameState == EGameState.Running;
    }

    public bool IsCountDown => gameState == EGameState.Countdown;

    public bool IsPaused()
    {
        return gameState == EGameState.Paused;
    }

    public void DisableMenus()
    {
        this.GameOverPanel.SetActive(false);
        this.PausePanel.SetActive(false);
    }

    public void ContinueGame()
    {
        this.DisableMenus();
        gameState = EGameState.Running;
        Time.timeScale = 1;
    }

    public void ShowSettings()
    {
        Debug.Log("Settings Requested");
    }

    public void ShowHome()
    {
        Debug.Log("Settings Requested");
    }

    public void FinishLevel()
    {
        Debug.Log("GameFinished");
        this.PauseGame();
        // show time
        // show Score (nr * )
        // show next start level
        // show level select 
        // show go home
    }

    public void SetGameState(EGameState state)
    {
        this.gameState = state;
    }
}
