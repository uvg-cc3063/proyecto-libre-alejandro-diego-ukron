﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDetection : MonoBehaviour
{
    public List<Gun> weapons;
    public Gun WeaponToTake;
    public Gun currentWeapon;
    public bool canGrabWeapon = false;
    public int weaponNum = 1;

    public GameObject playerhand;

    // Start is called before the first frame update
    void Start()
    {
        //weapons = new List<Gun>();
        currentWeapon = weapons[0];
        DeactivateColliderOfDefoultfW();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (canGrabWeapon == true)
            {                
                WeaponToTake.gameObject.transform.SetParent(playerhand.transform);
                WeaponToTake.gameObject.transform.localPosition = new Vector3(-0.001134747f, 0.01148188f, 0.001966352f);                
                WeaponToTake.gameObject.transform.localRotation = Quaternion.Euler(2.17f, -16.876f, 172.442f);
                WeaponToTake.gameObject.transform.localScale = new Vector3(0.02626913f, 0.02408004f, 0.02408004f);  
                WeaponToTake.GetComponent<Collider>().enabled = false;
                WeaponToTake.LightPointOff();
                currentWeapon = WeaponToTake;

                for (int i = 0; i < weapons.Count; i++)
                {
                    weapons[i].gameObject.SetActive(false);
                }
                currentWeapon.gameObject.SetActive(true);
                weapons.Add(WeaponToTake);
                canGrabWeapon = false;
                AudioManager.instance.PlaySfx(10);
                ChangeUIWeapon(currentWeapon);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if (weapons.Count > 1)
            {
                weaponNum++;
                if (weaponNum >= weapons.Count)
                {
                    weaponNum = 0;
                }
                
                //Debug.Log("numW: " + weaponNum);
                currentWeapon = weapons[weaponNum];
                for (int i = 0;i < weapons.Count;i++)
                {
                    weapons[i].gameObject.SetActive(false);
                }
                currentWeapon.gameObject.SetActive(true);
                currentWeapon.ActivateSound();
                ChangeUIWeapon(currentWeapon);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "weapon" && canGrabWeapon == false)
        {
            canGrabWeapon = true;
            WeaponToTake = other.gameObject.GetComponent<Gun>();
            //Debug.Log("tRIGGER ENTER");
        }
        if(other.tag == "ammo")
        {
            Ammo ammo = other.gameObject.GetComponent<Ammo>();
            if (ammo.type == Ammo.AmmoType.MORTARS)
            {
                for (int i = 0; i < weapons.Count; i++)
                {
                    if (weapons[i].type == Gun.GunType.MORTARS)
                    {
                        weapons[i].reloadWeapon(ammo.reloadAmount);
                        Debug.Log("Reloaded mortars");
                        ammo.DestroyAmmo();
                    }
                    else
                    {
                        Debug.Log("No Mortats in weapons list");
                    }
                }
            }
            else if (ammo.type == Ammo.AmmoType.SHOTGUN)
            {
                for (int i = 0; i < weapons.Count; i++)
                {
                    if (weapons[i].type == Gun.GunType.SHOTGUN)
                    {
                        weapons[i].reloadWeapon(ammo.reloadAmount);
                        Debug.Log("Reloaded shutgun");
                        ammo.DestroyAmmo();
                    }
                    else
                    {
                        Debug.Log("No shutgun in weapons list");
                    }
                }

            }
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "weapon")
        {
            canGrabWeapon = false;
            WeaponToTake = null;
            //Debug.Log("tRIGGER EXIT");
        }
        
    }

    private void DeactivateColliderOfDefoultfW()
    {
        weapons[0].gameObject.GetComponent<Collider>().enabled=false;

    }

    public void ChangeUIWeapon(Gun currentWeaponn)
    {
        if (currentWeaponn.type == Gun.GunType.SWORD)
        {
            UIManager.instance.ChangeWeapon(3);
        }
        else if (currentWeaponn.type == Gun.GunType.SHOTGUN)
        {
            UIManager.instance.ChangeWeapon(1);
        }
        else if (currentWeaponn.type == Gun.GunType.MORTARS)
        {
            UIManager.instance.ChangeWeapon(2);
        }
        else if (currentWeaponn.type == Gun.GunType.NORMAL)
        {
            UIManager.instance.ChangeWeapon(0);
        }
    }
   
}
