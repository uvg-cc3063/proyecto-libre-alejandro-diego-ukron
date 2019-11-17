using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public GameObject entrance;
    public GameObject MainCam, AnimatedCam;
    public GameObject UI,AIM;

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
            entrance.SetActive(false);
            BossController.instance.start = true;
            gameObject.SetActive(false);
        }
    }
}
