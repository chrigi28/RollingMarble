using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 
    public static EGameState GameState = EGameState.Paused;

    public GameObject GameOverPanel;
    public GameObject PausePanel;
    private GameObject currentLevel;
    
    public bool IsCountDown => GameState == EGameState.Countdown;


    void Awake()
    {
        Instance = this;
        this.DisableMenus();
    }

    void Start()
    {
    }

    void OnEnable()
    {
        
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        GameState = EGameState.Paused;
        this.GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public bool IsRunning()
    {
        return GameState == EGameState.Running;
    }

    public void DisableMenus()
    {
        this.GameOverPanel.SetActive(false);
        this.PausePanel.SetActive(false);
    }

    public void ContinueGame()
    {
        this.GameOverPanel.SetActive(false);
        this.PausePanel.SetActive(false);
        GameState = EGameState.Running;
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
}
