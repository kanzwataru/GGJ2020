using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairmanInput : MonoBehaviour
{
    RepairmanMotor motor;
    LadderMovement ladderMove;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<RepairmanMotor>();
        ladderMove = GetComponent<LadderMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        var input = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );

        if(ladderMove.CanClimb() && Input.GetAxis("Vertical") != 0 && !ladderMove.OnLadder()) {
            ladderMove.StartClimb();
        }

        if(ladderMove.OnLadder()) {
            ladderMove.Move(input);
            motor.enabled = false;
        }
        else {
            motor.enabled = true;
            motor.Move(input);
        }
    }
}
