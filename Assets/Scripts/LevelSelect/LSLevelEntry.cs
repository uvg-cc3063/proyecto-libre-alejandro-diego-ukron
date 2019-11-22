using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSLevelEntry : MonoBehaviour
{
    public string levelName, levelToCheck, displayName, levelNumber;
    private bool canLoadLevel, levelUnlocked;

    public GameObject mapPointActive, mapPointInactive;

    private bool levelLoading;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1 || levelToCheck == "")
        {
            mapPointActive.SetActive(true);
            mapPointInactive.SetActive(false);
            levelUnlocked = true;
        }
        else
        {
            mapPointActive.SetActive(false);
            mapPointInactive.SetActive(true);
            levelUnlocked = false;
        }

        if (PlayerPrefs.GetString("CurrentLevel") == levelName)
        {
            PlayerController.instance.transform.position = transform.position;
            LSResetPosition.instance.respawnPosition = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && canLoadLevel && levelUnlocked)
        {
            StartCoroutine(LevelLoadCo());
            levelLoading = true;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canLoadLevel = true;
            LSUIManager.instance.lNamePanel.SetActive(true);
            LSUIManager.instance.lNameText.text = displayName;
            if (levelNumber == "1")
            {
                LSUIManager.instance.imagenNivel1.SetActive(true);
                LSUIManager.instance.imagenNivel2.SetActive(false);
                LSUIManager.instance.imagenNivel3.SetActive(false);
            }
            else if (levelNumber == "2")
            {
                LSUIManager.instance.imagenNivel1.SetActive(false);
                LSUIManager.instance.imagenNivel2.SetActive(true);
                LSUIManager.instance.imagenNivel3.SetActive(false);
            }
            else if (levelNumber == "3")
            {
                LSUIManager.instance.imagenNivel1.SetActive(false);
                LSUIManager.instance.imagenNivel2.SetActive(false);
                LSUIManager.instance.imagenNivel3.SetActive(true);
            }
            if (PlayerPrefs.HasKey(levelName + "_coinsGold"))
            {
                LSUIManager.instance.coinsTextGold.text = PlayerPrefs.GetInt(levelName + "_coinsGold").ToString();

            }
            else
            {
                LSUIManager.instance.coinsTextGold.text = "???";

            }

            if (PlayerPrefs.HasKey(levelName + "_coinsSilver"))
            {

                LSUIManager.instance.coinsTextSilver.text = PlayerPrefs.GetInt(levelName + "_coinsSilver").ToString();

            }
            else
            {

                LSUIManager.instance.coinsTextSilver.text = "???";

            }

            if (PlayerPrefs.HasKey(levelName + "_coinsCupper"))
            {

                LSUIManager.instance.coinsTextCupper.text = PlayerPrefs.GetInt(levelName + "_coinsCupper").ToString();
            }
            else
            {
                LSUIManager.instance.coinsTextCupper.text = "???";
            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canLoadLevel = false;
            LSUIManager.instance.lNamePanel.SetActive(false);
            LSUIManager.instance.lNameText.text = displayName;
        }
    }

    public IEnumerator LevelLoadCo()
    {
        PlayerController.instance.stopMove = true;
        UIManager.instance.fadeToBlack = true;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(levelName);
        //Variable para el nivel
        PlayerPrefs.SetString("CurrentLevel", levelName);
    }
}