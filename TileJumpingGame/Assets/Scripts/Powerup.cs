﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float timeAlive; //Time available;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeAlive -= Time.deltaTime * 1.0f;
        //if(timeAlive < 0)
        //{
        //    Destroy(gameObject);
        //    PowerupSpawner.DecrementPowerUpCountOnScreen();
        //}

        //float y_rot = transform.rotation.y;
        //transform.Rotate(0.0f, 1.0f, 0.0f, Space.World);
    }

    public bool CheckDestruction()
    {
        return timeAlive < 0;
    }
}
