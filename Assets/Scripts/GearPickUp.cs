using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPickUp : MonoBehaviour
{
    public int value;
    public GameObject gearEffect;
    public int soundToPlay;
    public GearType type;

    public enum GearType
    {
        GOLD,
        SILVER,
        BRONCE
    }
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
        if (other.tag == "Player")
        {
            if(type == GearType.GOLD)
            {
                GameManager.instance.AddGoldGears(value);
            }
            else if (type == GearType.SILVER)
            {
                GameManager.instance.AddSilverGears(value);
            }
            else
            {
                GameManager.instance.AddBronceGears(value);
            }
            
            Destroy(gameObject);
            Instantiate(gearEffect, transform.position, transform.rotation);

            AudioManager.instance.PlaySfx(soundToPlay);
        }
    }
}
