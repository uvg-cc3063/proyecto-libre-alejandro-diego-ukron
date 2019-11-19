using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public GameObject entrance;
    public GameObject MainCam, AnimatedCam;
    public GameObject UI,AIM,BossHealthBar;

    public static BossActivator instance;

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

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //entrance.SetActive(false);
            entrance.gameObject.SetActive(true);
            BossController.instance.start = true;
            
            MainCam.gameObject.SetActive(false);
            AnimatedCam.gameObject.SetActive(true);
            UI.gameObject.SetActive(false);
            AIM.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void ReturnToMainCamera()
    {
        UI.gameObject.SetActive(true);        
        MainCam.gameObject.SetActive(true);
        AnimatedCam.gameObject.SetActive(false);
        ActivateBossHealthBar(true);
        if (PlayerController_s.instance.gund.currentWeapon.type == Gun.GunType.SWORD)
        {
            AIM.gameObject.SetActive(false);
        }
        else
        {
            AIM.gameObject.SetActive(true);
        }
    }

    public void ChangeToAnimatedCamera()
    {
        UI.gameObject.SetActive(false);
        AIM.gameObject.SetActive(false);
        MainCam.gameObject.SetActive(false);
        ActivateBossHealthBar(false);
        AnimatedCam.gameObject.SetActive(true);

    }
    public void ActivateBossHealthBar(bool activate)
    {
        BossHealthBar.gameObject.SetActive(activate);

    }

    public void EntranceOFF()
    {
        entrance.gameObject.SetActive(false);
    }

}
