using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    [UsedImplicitly]
    [SerializeField]
    private GameObject GameOverPanel;

    [UsedImplicitly]
    [SerializeField] 
    private GameObject PausePanel;

    [UsedImplicitly]
    [SerializeField] 
    private GameObject FinishPanel;

    [UsedImplicitly]
    [SerializeField] 
    private GameObject ContDownLabel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetGameOver(bool value)
    {
        this.GameOverPanel.SetActive(true);
    }

    public void TogglePause()
    {
        this.PausePanel.SetActive(GameManager.Instance.IsPaused());
    }

    public void DisableMenus()
    {
        this.GameOverPanel.SetActive(false);
        this.PausePanel.SetActive(false);
        this.FinishPanel.SetActive(false);
    }

    public void ShowFinish(float time)
    {
        var finishTime = this.FinishPanel.GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(f => f.name == "Time");
        finishTime.text = $"{time.ToString("F")}s";

        this.FinishPanel.SetActive(true);
    }

    public void StartGame()
    {
    }
}
