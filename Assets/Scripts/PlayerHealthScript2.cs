using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript2 : MonoBehaviour
{
    public static PlayerHealthScript2 instance;

    public int currentHealth;
    public int maxHealth;

    public bool shieldOn;

    public bool isDead;
    public GameObject slowmotion;
    public float timer;
    public float timer2;
    public GameObject whoWins;
    // Start is called before the first frame update


    public void Awake()
    {
        instance = this;

    }
    void Start()
    {
        currentHealth = maxHealth;
        shieldOn = false;

        UIHealth2.instance.healthSlider.maxValue = maxHealth;
        UIHealth2.instance.healthSlider.value = currentHealth;
        UIHealth2.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == true)
        {
            SlowmotionTimer();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("shieldOn");
            shieldOn = true;

        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            Debug.Log("shieldOff");
            shieldOn = false;
        }

        //Receive Punch
        if (Input.GetKeyDown(KeyCode.N))
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

        if (Input.GetKeyDown(KeyCode.M))
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

        UIHealth2.instance.healthSlider.value = currentHealth;
        UIHealth2.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }



    public void DamagePlayer1PunchReduced()
    {
        currentHealth = currentHealth - 2;

        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
            isDead = true;

        }

        UIHealth2.instance.healthSlider.value = currentHealth;
        UIHealth2.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
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

        UIHealth2.instance.healthSlider.value = currentHealth;
        UIHealth2.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }



    public void DamagePlayer1MissleReduced()
    {
        currentHealth = currentHealth - 4;

        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
            isDead = true;

        }

        UIHealth2.instance.healthSlider.value = currentHealth;
        UIHealth2.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }



    public void SlowmotionTimer()
    {
        StartCoroutine(timerr());
    }

    IEnumerator timerr()
    {
        slowmotion.SetActive(true);
        yield return new WaitForSeconds(timer);
        slowmotion.SetActive(false);
        isDead = false;
        yield return new WaitForSeconds(timer2);
        whoWins.SetActive(true);
    }
}
