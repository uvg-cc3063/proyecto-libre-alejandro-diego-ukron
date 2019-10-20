using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public bool killAtOnce = false;

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
        if (killAtOnce)
        {
            if (other.tag == "Player")
            {
                HealthManager.instance.HurtDeath();
            }
        }
        else
        {
            if (other.tag == "Player")
            {
                HealthManager.instance.Hurt();
            }
        }
    }
}