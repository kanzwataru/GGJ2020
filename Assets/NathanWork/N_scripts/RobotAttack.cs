﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAttack : MonoBehaviour
{
    //Punch
    //Missiles
    //Targeting
    public int punchDamage = 3;
    public bool isPlayerOne = true;

    public Animator robotAnimator;

    void Start()
    {
        robotAnimator = robotAnimator.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && isPlayerOne)
        {
            robotAnimator.SetTrigger("punch");
            // if active frames & if collision then deal damage

        }


    }
}
