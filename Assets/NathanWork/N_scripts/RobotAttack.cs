using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAttack : MonoBehaviour
{
    //Punch
    //Kick
    //Missiles
    //Targeting
    public Animator robotAnimator;
    public int punchDamage = 3;
    public int kickDamage = 2;
    public bool isPlayerOne = true;

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
