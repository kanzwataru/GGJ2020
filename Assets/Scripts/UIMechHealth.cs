using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMechHealth : MonoBehaviour
{
    public static UIMechHealth instance;

    public Slider healthSlide;
    public Text healthText;


    private void Awake()
    {
        instance = this;
    }

   
}
