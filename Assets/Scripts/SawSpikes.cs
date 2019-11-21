using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawSpikes : MonoBehaviour
{
    public bool killAtOnce = false;
    public enum InteractibleType
    {
        SPIKES,
        SAW
    }
    public InteractibleType type;


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
                if(type == InteractibleType.SAW)
                {
                    AudioManager.instance.PlaySfx(43);
                }
                else
                {
                    AudioManager.instance.PlaySfx(42);
                }
            }
        }
        else
        {
            if (other.tag == "Player")
            {
                HealthManager.instance.Hurt();
                if (type == InteractibleType.SAW)
                {
                    AudioManager.instance.PlaySfx(43);
                }
                else
                {
                    AudioManager.instance.PlaySfx(42);
                }
            }
        }
    }
}
