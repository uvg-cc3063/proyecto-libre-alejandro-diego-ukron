﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject HitEffect;
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
        if (other.tag == "Enemy")
        {
            Quaternion newRot = Quaternion.Euler(90,180,180);
            Instantiate(HitEffect,transform.position, newRot);
            
            other.GetComponent<EnemyHealthManager>().KillEnemy();
            AudioManager.instance.PlaySfx(38);
        }
        if (other.tag == "Tadd")
        {
            Quaternion newRot = Quaternion.Euler(90, 180, 180);
            Instantiate(HitEffect, transform.position, newRot);
            
            BossController.instance.DamageBoss();
            //other.GetComponent<BossController>().DamageBoss();
            AudioManager.instance.PlaySfx(38);
        }
        if (other.tag == "bossShield")
        {
            Quaternion newRot = Quaternion.Euler(90, 180, 180);
            Instantiate(HitEffect, transform.position, newRot);

            //BossController.instance.DamageBoss();
            //other.GetComponent<BossController>().DamageBoss();
            AudioManager.instance.PlaySfx(40);
        }
    }
}
