using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HealthManager : MonoBehaviour
{

    public static HealthManager instance;

    public int currentHealth, maxHealth;

    public float invincibleLength = 2f;
    private float invincCounter; 

    

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
        if(invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;
            
            if(invincCounter < 1f)
            {
                PlayerController.instance.playerModel.SetActive(true);
            }
        }
    }

    public void Hurt()
    {

        if (invincCounter <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameManager.instance.Respawn();
            }
            else
            {
                PlayerController.instance.Knockback();
                invincCounter = invincibleLength;

                PlayerController.instance.playerModel.SetActive(false);
            }

            UpdateUI();

        }
    }

    public void ResetHealth()
    {

        currentHealth = maxHealth;

        UpdateUI();
    }
    public void AddHeath(int amountToHeal)
    {
        currentHealth += amountToHeal;
        if(currentHealth > maxHealth)
       {
            currentHealth = maxHealth;
        }

        UpdateUI();


    }

    public void UpdateUI()
    {
        UIManager.instance.healthText.text = currentHealth.ToString();
    }


}
