using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSBridge : MonoBehaviour
{
    // Start is called before the first frame update
    public string levelToUnlock;
    void Start()
    {
        if (PlayerPrefs.GetInt(levelToUnlock + "_unlocked") == 0)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}