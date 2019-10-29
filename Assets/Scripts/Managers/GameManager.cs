using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Vector3 respawnPosition;

    public GameObject deathEffect;

    public int currentGoldGears;
    public int currentSilverGears;
    public int currentBronceGears;

    public int levelEndSFX= 8;

    public string levelToLoad;

    public bool isRespawning;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //respawnPosition = //PlayerController_s.instance.transform.position;
        respawnPosition = PlayerController.instance.transform.position;

        //AddCoins(0);
    }

    // Update is called once per frame
    void Update()
    {
        //PAUSE MENU
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnPause();
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
        HealthManager.instance.PlayerKilled();        
    }

    /** A coroutine can be called wherever you want in your code, though the coroutine starts, 
     * the code after its call keep going at its original order at the same time, in the same frame.
     */
    public IEnumerator RespawnCo()
    {
        CameraController.instance.theCMBrain.enabled = false;

        UIManager.instance.fadeToBlack = true;

        //Instantiate(deathEffect, PlayerController.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerController.instance.transform.rotation);
        yield return new WaitForSeconds(3f); //after two seconds the player is respawn to its original position.

        PlayerController.instance.gameObject.SetActive(false);
        isRespawning = true;
        HealthManager.instance.ResetHealth();
        UIManager.instance.fadeFromBlack = true;

        PlayerController.instance.transform.position = respawnPosition;
        CameraController.instance.theCMBrain.enabled = true;

        PlayerController.instance.gameObject.SetActive(true);
        PlayerController.instance.stopMove = false;

    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
        Debug.Log("SpawnPoint set");
    }

    //FOR GEARS
    public void AddGoldGears(int gearsToAdd)
    {
        currentGoldGears += gearsToAdd;
        UIManager.instance.gearGoldText.text = currentGoldGears.ToString();
    }

    public void AddSilverGears(int gearsToAdd)
    {
        currentSilverGears += gearsToAdd;
        UIManager.instance.gearSilverText.text = currentSilverGears.ToString();
    }

    public void AddBronceGears(int gearsToAdd)
    {
        currentBronceGears += gearsToAdd;
        UIManager.instance.gearBronceText.text = currentBronceGears.ToString();
    }
    //------------------

    public void PauseUnPause()
    {
        if (UIManager.instance.PauseScreen.activeInHierarchy)
        {
            UIManager.instance.PauseScreen.SetActive(false);
            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIManager.instance.PauseScreen.SetActive(true);
            UIManager.instance.CloseOptions();
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public IEnumerator LevelEndCo()
    {
        //AudioManager.instance.PlayMusic(levelEndMusic);
        AudioManager.instance.StopAllSFX();
        PlayerController.instance.stopMove = true;
        UIManager.instance.fadeToBlack = true;

        yield return new WaitForSeconds(4f);
        Debug.Log("Level ended");

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);

        //for coins
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_coinsGold"))
        {
            if (currentGoldGears > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_coinsGold"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coinsGold", currentGoldGears);
            }
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coinsGold", currentGoldGears);
        }
        //---------

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_coinsSilver"))
        {
            if (currentSilverGears > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_coinsSilver"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coinsSilver", currentSilverGears);
            }
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coinsSilver", currentSilverGears);
        }
        //-
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_coinsCupper"))
        {
            if (currentBronceGears > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_coinsCupper"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coinsCupper", currentBronceGears);
            }
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coinsCupper", currentBronceGears);
        }

        SceneManager.LoadScene(levelToLoad);

    }
}