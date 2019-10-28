using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUIManager : MonoBehaviour
{
    public static LSUIManager instance;
    // Start is called before the first frame update
    public Text lNameText;
    public GameObject lNamePanel;

    public Text coinsTextGold, coinsTextSilver, coinsTextCupper;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

    }
}
