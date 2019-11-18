using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamagePoint : MonoBehaviour
{
    private float Timer = 2f;
    private float elapsedTime;
    private bool Triggered = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Triggered == true)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > Timer)
            {
                elapsedTime = 0;
                Triggered = false;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Triggered == false)
        {
            if (other.tag == "nshot")
            {
                BossController.instance.DamageBoss();
                other.GetComponent<Proyectile>().DestroyOnHit();
            }
            if (other.tag == "mortar")
            {
                BossController.instance.DamageBoss();
                other.GetComponent<Mortar>().DestroyOnHit();
            }
            if (other.tag == "gshot")
            {
                BossController.instance.DamageBoss();
                other.GetComponent<Proyectile>().DestroyOnHit();
            }
            if (other.tag == "sword")
            {
                BossController.instance.DamageBoss();

            }
            Triggered = true;
        }       
        
    }
}
