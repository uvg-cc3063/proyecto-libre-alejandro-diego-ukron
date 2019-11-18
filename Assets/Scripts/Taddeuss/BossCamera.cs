using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCamera : MonoBehaviour
{
    private Animator anim;
    private bool IntroOnce = false;
    private bool SwordOnce = false;

    public static BossCamera instance;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(BossController.instance.currentPhase == BossController.BossPhase.changeToSword)
        {
            if (SwordOnce == false)
            {
                anim.SetTrigger("Sword");
                SwordOnce = true;
            }
        }
        if (BossController.instance.currentPhase == BossController.BossPhase.intro)
        {
            if (IntroOnce == false)
            {                
                anim.SetTrigger("Intro");
                IntroOnce = true;
            }
        }

    }

    public void IsdeathFalse()
    {
        anim.SetBool("isDeath",false);
    }
}
