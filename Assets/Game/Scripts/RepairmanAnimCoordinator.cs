using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairmanAnimCoordinator : MonoBehaviour
{
    public Transform visuals;

    RepairmanInput rinput;
    RepairmanMotor motor;
    LadderMovement ladderMove;
    Animator       animator;

    float lastXPos;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<RepairmanMotor>();
        ladderMove = GetComponent<LadderMovement>();
        animator = visuals.GetComponent<Animator>();
        rinput = GetComponent<RepairmanInput>();

        lastXPos = transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isRunning", motor.IsRunning());
        animator.SetBool("isJumping", motor.IsJumping());
        animator.SetBool("isClimbing", ladderMove.OnLadder());

        float xPos = transform.localPosition.x;

        /*
        if(xPos > lastXPos) {
            visuals.transform.localScale = new Vector3(1, 1, 1);
        }
        else if(xPos < lastXPos) {
            visuals.transform.localScale = new Vector3(1, 1, -1);
        }
        */

        if(rinput.facingDir != 0) {
            visuals.transform.localScale = new Vector3(1, 1, rinput.facingDir);
        }

        lastXPos = xPos;
    }
}
