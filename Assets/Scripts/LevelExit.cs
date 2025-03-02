﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public Animator anim;
    //private Vector3 offset;
    public bool animationStarted = false;
    public Transform target;
    public GameObject effect;
    public float acivateDistance;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //offset = transform.position - target.position;
        float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);
        //if (Mathf.Abs(offset.x) < 7.5 && animationStarted == false)
        if (distanceToPlayer < acivateDistance && animationStarted == false)
        {
            anim.SetTrigger("Hit");
            effect.gameObject.SetActive(true);
            animationStarted = true;
            AudioManager.instance.PlaySfx(18);
        }
        //Debug.Log(offset.x + " / " + offset.y + " / " + offset.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(GameManager.instance.LevelEndCo());
        }
    }
}
