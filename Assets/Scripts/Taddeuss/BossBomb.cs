﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBomb : MonoBehaviour
{
    public GameObject ExplotionEffect;
    public float elapsedTime;
    public float TimeToExplotion = 1f;
    public bool Grounded = false;

    // Use this for initialization
    void Start()
    {
        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToCamera = Vector3.Distance(transform.position, CameraController.instance.transform.position);
        if (distanceToCamera > 100)
        {
            Destroy(gameObject);
        }
        if (Grounded)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > TimeToExplotion)
            {
                Instantiate(ExplotionEffect, transform.position, transform.rotation);
                AudioManager.instance.PlaySfx(35);
                elapsedTime = 0;
                GetComponents<Collider>()[0].enabled = true;
                GetComponents<Collider>()[1].enabled = true;
                GetComponents<Collider>()[2].enabled = true;
                StartCoroutine(ExplosionTriggerActiveTime());
            }
        }
        
    }

    public void DestroyOnHit()
    {
        GetComponents<Collider>()[0].enabled = true;
        GetComponents<Collider>()[1].enabled = true;
        GetComponents<Collider>()[2].enabled = true;
        Instantiate(ExplotionEffect, transform.position, transform.rotation);
        AudioManager.instance.PlaySfx(35);
        //Destroy(gameObject);
        StartCoroutine(ExplosionTriggerActiveTimeHit());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ground")
        {
            Grounded = true;
        }
        if (other.tag == "Player")
        {
            HealthManager.instance.Hurt();
            Instantiate(ExplotionEffect, transform.position, transform.rotation);
            AudioManager.instance.PlaySfx(35);
            GetComponents<Collider>()[0].enabled = true;
            GetComponents<Collider>()[1].enabled = true;
            GetComponents<Collider>()[2].enabled = true;
            StartCoroutine(ExplosionTriggerActiveTimeHit());
        }
    }

    public IEnumerator ExplosionTriggerActiveTime()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
    public IEnumerator ExplosionTriggerActiveTimeHit()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
