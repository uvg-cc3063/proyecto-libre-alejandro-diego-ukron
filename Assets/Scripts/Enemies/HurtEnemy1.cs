﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Destroyable")
        {
            other.GetComponent<EnemyHealthManager1>().TakeDamage();
        }
        
    }
}