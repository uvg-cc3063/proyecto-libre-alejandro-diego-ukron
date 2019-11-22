using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Variables para el nivel
    public string firstLevel, levelSelect;

    public GameObject continueButton, canvasCreditos, canvasNormal;

    public string[] levelNames;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Continue"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            ResetProgress();
        }

        canvasCreditos.SetActive(false);
        canvasNormal.SetActive(true);
    }

    // Update is called once per frame
    void Update() { }

    public void NewGame()
    {
        SceneManager.LoadScene(firstLevel);
        PlayerPrefs.SetInt("Continue", 0);
        PlayerPrefs.SetString("CurrentLevel", firstLevel);

        ResetProgress();
    }

    public void Continue()
    {
        SceneManager.LoadScene(levelSelect);
    }

    public void Creditos()
    {
        canvasCreditos.SetActive(true);
        canvasNormal.SetActive(false);
    }

    public void RegresarMenuNormal()
    {
        canvasCreditos.SetActive(false);
        canvasNormal.SetActive(true);

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ResetProgress()
    {
        for (int i = 0; i < levelNames.Length; i++)
        {
            PlayerPrefs.SetInt(levelNames[i] + "_unlocked", 0);
        }
    }
}