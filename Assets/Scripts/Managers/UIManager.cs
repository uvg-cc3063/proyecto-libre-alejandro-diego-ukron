using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image blackScreen;
    public float fadeSpeed = 0.65f;
    public bool fadeToBlack, fadeFromBlack;

    public Text healthText;
    public Image healthImage;

    public Text gearGoldText, gearBronceText, gearSilverText;

    public GameObject PauseScreen, optionsScreen,controlsScreen;

    public Slider musicVolSlider, sfxVolSlider;

    public string mainMenu, levelSelect;

    public Image[] weaponImages;
    public Image AimImage;
    public Image ArmorBar;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (fadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b,
                Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }

        if (fadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b,
                Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                fadeFromBlack = false;
            }
        }
    }

    public void Resume()
    {
        GameManager.instance.PauseUnPause();
    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void OpenControls()
    {
        controlsScreen.SetActive(true);
    }

    public void CloseControls()
    {
        controlsScreen.SetActive(false);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1;
    }

    public void SetMusicLevel()
    {
        AudioManager.instance.SetMusicLevel();
    }

    public void SetSfxLevel()
    {
        AudioManager.instance.SetSfxLevel();
    }

    public void ChangeWeapon(int WeaponIndex)
    {
        if(WeaponIndex == 3)
        {
            AimImage.gameObject.SetActive(false);
        }
        else
        {
            AimImage.gameObject.SetActive(true);
        }

        for(int i = 0; i < weaponImages.Length; i++)
        {
            weaponImages[i].gameObject.SetActive(false);
        }
        weaponImages[WeaponIndex].gameObject.SetActive(true);
    }

    public void UpdateAmmoBar(int currentAmmo, int MaxAmount)
    {
        float conv = currentAmmo * 1f / MaxAmount;
        //Debug.Log("conv; " + conv);
        ArmorBar.fillAmount = conv;
    }


}