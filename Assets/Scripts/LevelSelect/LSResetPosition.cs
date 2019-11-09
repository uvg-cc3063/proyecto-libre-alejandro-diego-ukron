using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSResetPosition : MonoBehaviour
{
    public static LSResetPosition instance;

    public Vector3 respawnPosition;

    private void Awake()
    {
        instance = this;
    }

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
        if (other.CompareTag("Player"))
        {
            PlayerController_s.instance.gameObject.SetActive(false);
            PlayerController_s.instance.transform.position =respawnPosition;
            PlayerController_s.instance.gameObject.SetActive(true);
        }
    }
}