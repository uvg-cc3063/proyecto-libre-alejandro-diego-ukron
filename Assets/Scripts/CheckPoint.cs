using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject cpON, cpOFF;
    public int soundToPlay;

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
            GameManager.instance.SetSpawnPoint(transform.position); //SET THE NEW POSITION OF THE SPAWN POINT IN GAMEMANAGER

            CheckPoint[] allCP = FindObjectsOfType<CheckPoint>();
            //AudioManager.instance.PlaySFX(soundToPlay);
            for (int i = 0; i < allCP.Length; i++) //Setting all checkpoints to OFF except current checkpoint.
            {
                allCP[i].cpOFF.SetActive(true);
                allCP[i].cpON.SetActive(false);
            }

            cpOFF.SetActive(false);
            cpON.SetActive(true);

        }
    }
}
