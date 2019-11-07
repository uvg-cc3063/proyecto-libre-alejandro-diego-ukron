using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour
{    
    [SerializeField]
    [Range(0.1f, 1.5f)]
    private float fireRate = 0.3f;

    public int maxAmmo = 10;
    public int currentAmmo;
    //public float reloadTime = 1f;

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

    void Start()
    {
        /*if(type == GunType.SHOTGUN || type == GunType.MORTARS)
        {
            currentAmmo = maxAmmo;
        }  */      
    }

    void Update()
    {
        if (GetComponentInParent<PlayerController_s>() != null)
        {
            timer += Time.deltaTime;
            if (timer >= fireRate)
            {
                                   
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (currentAmmo > 0)
                    {
                        timer = 0f;
                        FireGun();
                    }
                    else
                    {
                        AudioManager.instance.PlaySfx(24);
                    }
                }                               
               
            }
        }        
    }

    private void FireGun()
    {        
        if (type == GunType.SWORD)
        {
            targetImage.gameObject.SetActive(false);
            PlayerController_s.instance.Sword();
            AudioManager.instance.PlaySfx(21);
        }
        else if (type == GunType.SHOTGUN)
        {
            currentAmmo--;
            targetImage.gameObject.SetActive(true);

            //Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f);
            PlayerController_s.instance.Shot();
            muzzleParticle.Play();
            AudioManager.instance.PlaySfx(20);
            //Ray ray = new Ray(firePoint.position, firePoint.forward);
            Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
            RaycastHit hitInfo;

            //Disparo
            float zpos = Random.Range(-1f, 1f);
            float zpos2 = Random.Range(-1f, 1f);
            float zpos3 = Random.Range(-1f, 1f);
            float zpos4 = Random.Range(-1f, 1f);
            float ypos = Random.Range(-0.5f, 0.5f);
            float ypos2 = Random.Range(-0.5f, 0.5f);
            float ypos3 = Random.Range(-0.5f, 0.5f);
            float ypos4 = Random.Range(-0.5f, 0.5f);
            float xpos = Random.Range(-0.5f, 0.5f);
            float xpos2 = Random.Range(-0.5f, 0.5f);
            float xpos3 = Random.Range(-0.5f, 0.5f);
            float xpos4 = Random.Range(-0.5f, 0.5f);

            Vector3 newPosition = new Vector3(firePoint.position.x + xpos, firePoint.position.y + ypos, firePoint.position.z + zpos);
            Rigidbody shotPrefab = Instantiate(Proyectile, newPosition, Quaternion.identity);
            Vector3 newPosition2 = new Vector3(firePoint.position.x + xpos2, firePoint.position.y + ypos2, firePoint.position.z + zpos2);
            Rigidbody shotPrefab2 = Instantiate(Proyectile, newPosition2, Quaternion.identity);
            Vector3 newPosition3 = new Vector3(firePoint.position.x + xpos3, firePoint.position.y + ypos3, firePoint.position.z + zpos3);
            Rigidbody shotPrefab3 = Instantiate(Proyectile, newPosition3, Quaternion.identity);
            Vector3 newPosition4 = new Vector3(firePoint.position.x + xpos4, firePoint.position.y + ypos4, firePoint.position.z + zpos4);
            Rigidbody shotPrefab4 = Instantiate(Proyectile, newPosition4, Quaternion.identity);

            if (Physics.Raycast(ray, out hitInfo, 100))
            {
                shotPrefab.AddForce((hitInfo.point - firePoint.position).normalized * 100 * shotSpeed);
                shotPrefab2.AddForce((hitInfo.point - firePoint.position).normalized * 100 * shotSpeed);
                shotPrefab3.AddForce((hitInfo.point - firePoint.position).normalized * 100 * shotSpeed);
                shotPrefab4.AddForce((hitInfo.point - firePoint.position).normalized * 100 * shotSpeed);

            }
        }else if (type == GunType.MORTARS)
        {
            targetImage.gameObject.SetActive(true);
            currentAmmo--;
            //Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f);
            PlayerController_s.instance.Shot();
            muzzleParticle.Play();
            AudioManager.instance.PlaySfx(19);
            Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
            RaycastHit hitInfo;

            //Disparo
            Rigidbody shotPrefab;
            shotPrefab = Instantiate(Proyectile, firePoint.position, Quaternion.identity);

            if (Physics.Raycast(ray, out hitInfo, 100))
            {
                shotPrefab.AddForce((hitInfo.point - firePoint.position).normalized * 100 * shotSpeed);
            }
        }
        else
        {
            targetImage.gameObject.SetActive(true);
            //Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f);
            PlayerController_s.instance.Shot();
            muzzleParticle.Play();
            AudioManager.instance.PlaySfx(19);
            Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f);
            RaycastHit hitInfo;

            //Disparo
            Rigidbody shotPrefab;
            shotPrefab = Instantiate(Proyectile, firePoint.position, Quaternion.identity);
            
            if (Physics.Raycast(ray, out hitInfo, 100))
            {
                shotPrefab.AddForce((hitInfo.point - firePoint.position).normalized *100* shotSpeed);

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

    public void ActivateSound()
    {
        if (type == GunType.SWORD)
        {
            AudioManager.instance.PlaySfx(23);
        }
        else
        {
            AudioManager.instance.PlaySfx(22);
        }
            
    }

    public void reloadWeapon(int reloadAmount)
    {
        currentAmmo += reloadAmount;
        if (currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
        
    }
}