﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : BaseScript
{
    public GameObject Camera;
    public GameObject Player;

    public Vector3 Offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.Camera.transform.position = this.Player.transform.position + this.Offset;
    }
}
