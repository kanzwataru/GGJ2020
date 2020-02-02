using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    public static UIHealth instance;
    public Slider healthSlider;
    public Text healthText;


    private void Awake()
    {
        instance = this;
    }
    
}
