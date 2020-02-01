using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    public static PlayerHealthScript instance;

    public int currentHealth;
    public int maxHealth;
    // Start is called before the first frame update


    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;

        UIHealth.instance.healthSlider.maxValue = maxHealth;
        UIHealth.instance.healthSlider.value = currentHealth;
        UIHealth.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DamagePlayer1();
        }
    }


    public void DamagePlayer1()
    {
        currentHealth--;

        if(currentHealth <= 0)
        {
            Debug.Log("Dead");
        }


        UIHealth.instance.healthSlider.value = currentHealth;
        UIHealth.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
