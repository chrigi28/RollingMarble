using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.LoadPlayerData();
    }
}
