using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : MonoBehaviour
{
    public GameObject ExplotionEffect;
    public float elapsedTime;
    public float TimeToExplotion;
    public bool Grounded = false;

    // Use this for initialization
    void Start()
    {
        transform.localRotation = Quaternion.Euler(0, -90, 0);
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
            if(elapsedTime > TimeToExplotion)
            {
                Instantiate(ExplotionEffect, transform.position, transform.rotation);
                elapsedTime = 0;
                GetComponents<Collider>()[0].enabled = true;
                GetComponents<Collider>()[1].enabled = true;
                GetComponents<Collider>()[2].enabled = true;                
                Destroy(gameObject);

            }
        }
    }

    public void DestroyOnHit()
    {
        GetComponents<Collider>()[0].enabled = true;
        GetComponents<Collider>()[1].enabled = true;
        GetComponents<Collider>()[2].enabled = true;
        Instantiate(ExplotionEffect, transform.position, transform.rotation);        
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ground")
        {
            Grounded = true;
        }
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthManager>().KillEnemy();
            Instantiate(ExplotionEffect, transform.position, transform.rotation);
            GetComponents<Collider>()[0].enabled = true;
            GetComponents<Collider>()[1].enabled = true;
            GetComponents<Collider>()[2].enabled = true;
            Destroy(gameObject);            
        }
    }
}
