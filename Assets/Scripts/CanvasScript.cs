using System.Collections;
using System.Collections.Generic;
using Assets;
using JetBrains.Annotations;
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

    public void ShowFinish()
    {
        this.FinishPanel.SetActive(true);
    }
}
