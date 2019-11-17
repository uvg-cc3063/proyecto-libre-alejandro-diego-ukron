using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamagePoint : MonoBehaviour
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
        if (other.tag == "nshot")
        {
            BossController.instance.DamageBoss();
            other.GetComponent<Proyectile>().DestroyOnHit();
        }
        if (other.tag == "mortar")
        {
            BossController.instance.DamageBoss2();
            other.GetComponent<Mortar>().DestroyOnHit();
        }
        if (other.tag == "gshot")
        {
            BossController.instance.DamageBoss();
            other.GetComponent<Proyectile>().DestroyOnHit();
        }
        if (other.tag == "sword")
        {
            BossController.instance.DamageBoss2();
            
        }
    }
}
