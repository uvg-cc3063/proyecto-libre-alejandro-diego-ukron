using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager1 : MonoBehaviour
{
    //varoables para la salud del enemigo
    public int maxHealth = 1;

    private int currentHealth;

    public int deathSound;

    public GameObject deathEffect, itemToDrop;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            AudioManager.instance.PlaySfx(deathSound);
            Destroy(gameObject);
            PlayerController_s.instance.Bounce();
            var transform1 = transform;
            var position = transform1.position;
            var rotation = transform1.rotation;
            Instantiate(deathEffect, position + new Vector3(0, 1.2f, 0f), rotation);
            Instantiate(itemToDrop, position + new Vector3(0, 0.5f, 0f), rotation);
        }
    }
}