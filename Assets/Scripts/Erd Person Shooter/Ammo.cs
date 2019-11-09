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

    public void DestroyAmmo()
    {
        Destroy(gameObject);
        AudioManager.instance.PlaySfx(25);
    }
}
