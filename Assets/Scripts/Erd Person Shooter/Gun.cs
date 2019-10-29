using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour
{    
    [SerializeField]
    [Range(0.1f, 1.5f)]
    private float fireRate = 0.3f;

    [SerializeField]
    [Range(1, 10)]
    private int damage = 1;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private ParticleSystem muzzleParticle;

    private float timer;

    public Rigidbody Proyectile;
    public float shotSpeed;

    public enum GunType
    {
        NORMAL,
        SHOTGUN,
        MORTARS,
        SWORD
    }

    public GameObject targetImage;
    public GunType type;

    void Update()
    {
        if (GetComponentInParent<PlayerController_s>() != null)
        {
            timer += Time.deltaTime;
            if (timer >= fireRate)
            {
                /*if (Input.GetButton("Fire1"))
                {
                    timer = 0f;
                    FireGun();
                }*/
                //if (Input.GetKeyDown(KeyCode.X))
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    timer = 0f;
                    FireGun();
                }
                //else if (Input.GetKeyUp(KeyCode.X))
                else if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    //PlayerController_s.instance.stopShot();
                }
                /*else 
                {
                    PlayerController_s.instance.stopShot();
                }*/
            }
        }        
    }

    private void FireGun()
    {        
        if (type == GunType.SWORD)
        {
            targetImage.gameObject.SetActive(false);
            PlayerController_s.instance.Sword();
        }
        else if (type == GunType.SHOTGUN)
        {
            targetImage.gameObject.SetActive(true);

            //Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f);
            PlayerController_s.instance.Shot();
            muzzleParticle.Play();
            AudioManager.instance.PlaySfx(19);
            //Ray ray = new Ray(firePoint.position, firePoint.forward);
            Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
            RaycastHit hitInfo;

            //Disparo
            //shotPrefab = Instantiate(Proyectile,firePoint.position,Quaternion.identity);
            //shotPrefab.AddForce(firePoint.forward * 100 * shotSpeed);
            Rigidbody shotPrefab = Instantiate(Proyectile, firePoint.position, Quaternion.identity);
            Vector3 newPosition2 = new Vector3(firePoint.position.x + 0.5f, firePoint.position.y, firePoint.position.z);
            Rigidbody shotPrefab2 = Instantiate(Proyectile, newPosition2, Quaternion.identity);
            Vector3 newPosition3 = new Vector3(firePoint.position.x - 0.5f, firePoint.position.y, firePoint.position.z);
            Rigidbody shotPrefab3 = Instantiate(Proyectile, newPosition3, Quaternion.identity);
            //Vector3 newDirection = new Vector3(ray.direction.x, ray.direction.y + 50, ray.direction.z); //se trato de ajustar mira
            shotPrefab.AddForce(ray.direction * 100 * shotSpeed);
            shotPrefab2.AddForce(ray.direction * 100 * shotSpeed);
            shotPrefab3.AddForce(ray.direction * 100 * shotSpeed);
        }
        else
        {
            targetImage.gameObject.SetActive(true);

            //Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f);
            PlayerController_s.instance.Shot();
            muzzleParticle.Play();
            AudioManager.instance.PlaySfx(19);
            //Ray ray = new Ray(firePoint.position, firePoint.forward);
            Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
            RaycastHit hitInfo;

            //Disparo
            Rigidbody shotPrefab;
            //shotPrefab = Instantiate(Proyectile,firePoint.position,Quaternion.identity);
            //shotPrefab.AddForce(firePoint.forward * 100 * shotSpeed);
            shotPrefab = Instantiate(Proyectile, firePoint.position, Quaternion.identity);
            //Vector3 newDirection = new Vector3(ray.direction.x, ray.direction.y + 50, ray.direction.z); //se trato de ajustar mira
            shotPrefab.AddForce(ray.direction * 100 * shotSpeed);
            //

            if (Physics.Raycast(ray, out hitInfo, 100))
            {
                //var health = hitInfo.collider.GetComponent<Health>();
                //Destroy(hitInfo.collider.gameObject);
                /*if (health != null)
                    health.TakeDamage(damage);*/
            }
        }                 
    }

    public IEnumerator StopAiming()
    {

        yield return new WaitForSeconds(4f);
        Debug.Log("stopAiming");

        //Instantiate(itemToDrop, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);

    }
}