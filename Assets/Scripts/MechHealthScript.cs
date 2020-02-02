using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechHealthScript : MonoBehaviour
{
    public static MechHealthScript instance;

    public int currentHealth;
    public int maxHealth;

    public Animator anim;
    // Start is called before the first frame update


    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;

        UIMechHealth.instance.healthSlide.maxValue = maxHealth;
        UIMechHealth.instance.healthSlide.value = currentHealth;
        UIMechHealth.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            
            {
                DamagePlayer2();
            }
        }
    }


    public void DamagePlayer2()
    {
        currentHealth--;
        anim.SetTrigger("takeDamage");

        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
        }


        UIMechHealth.instance.healthSlide.value = currentHealth;
        UIMechHealth.instance.healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }

    
        




}
