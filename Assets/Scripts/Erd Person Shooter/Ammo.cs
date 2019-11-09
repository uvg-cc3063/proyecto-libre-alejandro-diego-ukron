using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int reloadAmount;
    public enum AmmoType
    {
        SHOTGUN,
        MORTARS
    }
    public AmmoType type;

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
        /*if (other.tag == "Player")
        {
            List<Gun> playerWeapons = other.GetComponent<GunDetection>().weapons;
            for (int i=0; i<playerWeapons.Count; i++)
            {
                if(playerWeapons[i].type == Gun.GunType.MORTARS && type == AmmoType.MORTARS)
                {
                    
                    Debug.Log("MortarsAmmoDestroyed");
                }
                else if (playerWeapons[i].type == Gun.GunType.SHOTGUN && type == AmmoType.MORTARS)
                {
                   
                    Debug.Log("ShutGunAmmoDestroyed");
                }
                else
                {
                    Debug.Log("Weapon not found in players weapon list");
                }
            }            
            
        }*/
    }

    public void DestroyAmmo()
    {
        Destroy(gameObject);
        AudioManager.instance.PlaySfx(25);
    }
}
