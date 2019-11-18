using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSounds : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playBossSfx(int SfxToPlay)
    {
        AudioManager.instance.PlaySfx(SfxToPlay);
    }

    public void BossShot()
    {
        BossController.instance.ShotBoss();
    }

    public void DestroyBoss()
    {
        BossController.instance.DestroyBoss();
    }


}
