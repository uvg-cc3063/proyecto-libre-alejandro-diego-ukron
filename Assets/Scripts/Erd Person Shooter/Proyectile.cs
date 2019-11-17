using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour {

    public GameObject DestroyEffect;

    // Use this for initialization
    void Start () {
        transform.localRotation = Quaternion.Euler(0, -90, 0);
    }
	
	// Update is called once per frame
	void Update () {
        float distanceToCamera = Vector3.Distance(transform.position, CameraController.instance.transform.position);
        if (distanceToCamera > 100)
        {
            Destroy(gameObject);
        }
    }

    public void DestroyOnHit()
    {                
        Instantiate(DestroyEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Environment")
        {
            DestroyOnHit();
        }
        if (other.tag == "Enemy")
        {
            DestroyOnHit();
            other.GetComponent<EnemyHealthManager>().TakeDamage();
        }
        if (other.tag == "bossShield")
        {
            DestroyOnHit();
        }
    }
}
