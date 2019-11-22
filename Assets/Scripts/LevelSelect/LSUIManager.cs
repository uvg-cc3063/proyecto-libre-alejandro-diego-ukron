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

    public GameObject imagenNivel1, imagenNivel2, imagenNivel3;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        imagenNivel1.SetActive(false);
        imagenNivel2.SetActive(false);
        imagenNivel3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
