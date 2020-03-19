using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour
{
    private GameManager gameManager;

    protected GameManager GameManager
    {
        get => this.gameManager ?? (this.gameManager = GameManager.Instance);
        ////get => this.gameManager ?? (this.gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>());
        set { this.gameManager = value; }
    }
}
