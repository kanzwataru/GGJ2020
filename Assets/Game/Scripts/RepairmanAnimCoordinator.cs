using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairmanAnimCoordinator : MonoBehaviour
{
    public Transform visuals;

    RepairmanMotor motor;
    LadderMovement ladderMove;
    Animator       animator;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<RepairmanMotor>();
        ladderMove = GetComponent<LadderMovement>();
        animator = visuals.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isRunning", motor.IsRunning());
        animator.SetBool("isJumping", motor.IsJumping());
        animator.SetBool("isClimbing", ladderMove.OnLadder());
    }
}
