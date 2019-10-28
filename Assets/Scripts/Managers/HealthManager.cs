using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public int currentHealth, maxHealth;

    public float invincibleLength = 2f;
    private float invincCounter;

    public Sprite[] healthBarImages;

    public int soundDeath, soundDeath2, soundHurt;

    public Animator anim;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;

            for (int i = 0; i < PlayerController.instance.playerPieces.Length; i++)
            {
                if (Mathf.Floor(invincCounter * 5f) % 2 == 0) //even or odd number, for flashing
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
                else
                {
                    PlayerController.instance.playerPieces[i].SetActive(false);
                }

                if (invincCounter <= 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
            }
        }
    }

    public void Hurt()
    {
        if (invincCounter <= 0)
        {
            currentHealth -= 1;


            if (currentHealth <= 0)
            {
                AudioManager.instance.PlaySfx(soundDeath);
                AudioManager.instance.PlaySfx(soundDeath2);
                currentHealth = 0;
                anim.SetBool("isDeath", true);
                anim.SetTrigger("Death");
                PlayerController.instance.Knockback();
                PlayerController.instance.stopMove = true;
                GameManager.instance.Respawn();
            }
            else
            {
                AudioManager.instance.PlaySfx(soundHurt);
                anim.SetTrigger("Hurt");
                anim.SetBool("isDeath", false);
                PlayerController.instance.Knockback();
                invincCounter = invincibleLength;
            }

            UpdateUI();
        }
    }

    public void HurtDeath()
    {
        if (invincCounter <= 0)
        {
            currentHealth = 0;
            anim.SetBool("isDeath", true);
            anim.SetTrigger("Death");
            PlayerController.instance.Knockback();
            PlayerController.instance.stopMove = true;
            AudioManager.instance.PlaySfx(soundDeath);
            AudioManager.instance.PlaySfx(soundDeath2);

            GameManager.instance.Respawn();

            UpdateUI();
        }
    }

    public void ResetHealth()
    {
        anim.SetBool("isDeath", false);
        currentHealth = maxHealth;
        PlayerController.instance.stopMove = false;
        PlayerController_s.instance.stopMove = false;
        UIManager.instance.healthImage.enabled = true;
        UpdateUI();
    }

    public void AddHealth(int amountToHeal)
    {
        currentHealth += amountToHeal;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        UIManager.instance.healthText.text = currentHealth.ToString();
        switch (currentHealth)
        {
            case 5:
                UIManager.instance.healthImage.sprite = healthBarImages[4];
                break;
            case 4:
                UIManager.instance.healthImage.sprite = healthBarImages[3];
                break;
            case 3:
                UIManager.instance.healthImage.sprite = healthBarImages[2];
                break;
            case 2:
                UIManager.instance.healthImage.sprite = healthBarImages[1];
                break;
            case 1:
                UIManager.instance.healthImage.sprite = healthBarImages[0];
                break;
            case 0:
                UIManager.instance.healthImage.sprite = healthBarImages[5];
                break;
        }
    }

    public void PlayerKilled()
    {
        AudioManager.instance.PlaySfx(soundDeath);
        AudioManager.instance.PlaySfx(soundDeath2);
        anim.SetBool("isDeath", true);
        anim.SetTrigger("Death");
        currentHealth = 0;
        UpdateUI();
    }
}