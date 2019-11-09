using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public float maxHealth = 1;
    private float currentHealth;

    public int deathSound;

    public GameObject deathEffect, itemToDrop;

    public EnemyController enemycontrol;
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
            //AudioManager.instance.PlaySfx(deathSound);
            enemycontrol.currentState = EnemyController.AIState.isDeath;
            StartCoroutine(DestroyEnemy());
            AudioManager.instance.PlaySfx(deathSound);

            //PlayerController_s.instance.Bounce();

            //Instantiate(deathEffect, transform.position + new Vector3(0, 1.2f, 0), transform.rotation); //EXPLOSION

        }
        else if(currentHealth > 0)
        {
            enemycontrol.currentState = EnemyController.AIState.isBeingHited;
            Debug.Log("Bat Hited");
        }
    }

    public void KillEnemy()
    {
        currentHealth = 0;

            enemycontrol.currentState = EnemyController.AIState.isDeath;
            StartCoroutine(DestroyEnemy());
            AudioManager.instance.PlaySfx(deathSound);

            //PlayerController_s.instance.Bounce();

            //Instantiate(deathEffect, transform.position + new Vector3(0, 1.2f, 0), transform.rotation); //EXPLOSION

    }

    public IEnumerator DestroyEnemy()
    {        

        yield return new WaitForSeconds(4f);
        Debug.Log("EnemyDestroy");
        Destroy(gameObject);
        Instantiate(itemToDrop, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);

    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "weapon")
        {
            DestroyEnemy();

        }
    }*/
}
