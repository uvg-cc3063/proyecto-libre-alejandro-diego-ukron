using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPickUp : MonoBehaviour
{
    public int value;
    public GameObject gearEffect;
    public int soundToPlay;
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
        if (other.tag == "Player")
        {
            GameManager.instance.AddGears(value);
            Destroy(gameObject);
            Instantiate(gearEffect, transform.position, transform.rotation);

            //AudioManager.instance.PlaySFX(soundToPlay);
        }
    }
}
