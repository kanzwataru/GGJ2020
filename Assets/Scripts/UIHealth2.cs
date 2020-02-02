using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth2 : MonoBehaviour
{
    public static UIHealth2 instance;
    public Slider healthSlider;
    public Text healthText;


    private void Awake()
    {
        instance = this;
    }

}
