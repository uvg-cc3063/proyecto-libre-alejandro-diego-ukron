using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSResetPosition : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            PlayerController.instance.gameObject.SetActive(false);
            PlayerController.instance.transform.position = new Vector3(16.48f, 2, -3.12f);
            PlayerController.instance.gameObject.SetActive(true);
        }
    }
}