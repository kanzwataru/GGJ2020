using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    public static PlayerHealthScript instance;

    public int currentHealth;
    public int maxHealth;

    public bool shieldOn;

    public bool isDead;
    // Start is called before the first frame update


    public void Awake()
    {
        instance = this;
        
    }
    void Start()
    {
        currentHealth = maxHealth;
        shieldOn = false;

        UIHealth.instance.healthSlider.maxValue = maxHealth;
        UIHealth.instance.healthSlider.value = currentHealth;
        UIHealth.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("shieldOn");
            shieldOn = true;

        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            Debug.Log("shieldOff");
            shieldOn = false;
        }

        //Receive Punch
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (shieldOn == true)
            {
                DamagePlayer1PunchReduced();
            }
            else
            {
                DamagePlayer1Punch();   
            }

        }

        //Receive Missle

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (shieldOn == true)
            {
                DamagePlayer1MissleReduced();
            }
            else
            {
                DamagePlayer1Missle();
            }

        }

        if(isDead == true)
        {
            //SlowMotion
        }
    }

    //Punch Damages

    public void DamagePlayer1Punch()
    {
        currentHealth = currentHealth - 10;

        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
            isDead = true;
        }

        UIHealth.instance.healthSlider.value = currentHealth;
        UIHealth.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }



    public void DamagePlayer1PunchReduced()
    {
        currentHealth = currentHealth - 2;

        if(currentHealth <= 0)
        {
            Debug.Log("Dead");
            isDead = true;
        }

        UIHealth.instance.healthSlider.value = currentHealth;
        UIHealth.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    //Missle Damages

    public void DamagePlayer1Missle()
    {
        currentHealth = currentHealth - 20;

        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
            isDead = true;
        }

        UIHealth.instance.healthSlider.value = currentHealth;
        UIHealth.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }



    public void DamagePlayer1MissleReduced()
    {
        currentHealth = currentHealth - 4;

        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
            isDead = true;
        }

        UIHealth.instance.healthSlider.value = currentHealth;
        UIHealth.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
