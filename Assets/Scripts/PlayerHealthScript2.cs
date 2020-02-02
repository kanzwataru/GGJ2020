using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject whoWins2;


    public bool player2Dead = false;
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
        //UIHealth2.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
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
                DamagePlayer2PunchReduced();
            }
            else
            {
                DamagePlayer2Punch();
            }

        }

        //Receive Missle

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (shieldOn == true)
            {
                DamagePlayer2MissleReduced();
            }
            else
            {
                DamagePlayer2Missle();
            }

        }


    }

    //Punch Damages

    public void DamagePlayer2Punch()
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



    public void DamagePlayer2PunchReduced()
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

    public void DamagePlayer2Missle()
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



    public void DamagePlayer2MissleReduced()
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
        
        isDead = false;
        player2Dead = true;
        yield return new WaitForSeconds(timer2);
        whoWins.SetActive(true);
        //whoWins.SetActive(true);
        //whoWins2.SetActive(true);
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene("MenuScene");
    }
}
