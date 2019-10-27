﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    //Variables para el nivel
    public string firstLevel, levelSelect;

    public GameObject continueButton;

    // Start is called before the first frame update
    void Start () {
        if (PlayerPrefs.HasKey ("Continue")) {
            continueButton.SetActive (true);
        }
    }

    // Update is called once per frame
    void Update () { }

    public void NewGame () {
        SceneManager.LoadScene (firstLevel);
        PlayerPrefs.SetInt ("Continue", 0);
    }

    public void Continue () {
        SceneManager.LoadScene (levelSelect);
    }

    public void Quit () {
        Application.Quit ();
    }
}