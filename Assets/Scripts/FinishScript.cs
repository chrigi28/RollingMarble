using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : BaseScript
{
    private void OnTriggerEnter(Collider other)
    {
        this.GameManager.FinishLevel();
    }
}
