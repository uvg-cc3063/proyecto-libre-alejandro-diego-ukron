using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealthBar : MonoBehaviour
{
    public Image fillBar;


    public static BossHealthBar instance;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(BossController.instance == null)
        {
            Destroy(gameObject);
        }*/
    }

    public void UpdateHealthBar(int currentHealth, int MaxHealth)
    {
        float conv = currentHealth * 1f / MaxHealth;
        //Debug.Log("conv; " + conv);
        fillBar.fillAmount = conv;
    }

    public void DestroyBar()
    {
        Destroy(gameObject);
    }
}
