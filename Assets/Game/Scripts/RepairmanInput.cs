using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairmanInput : MonoBehaviour
{
    RepairmanMotor motor;
    BetterLadderMovement ladderMove;

    public int facingDir = 1;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<RepairmanMotor>();
        ladderMove = GetComponent<BetterLadderMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        var input = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical")
        );

        if(input.x != 0)
            facingDir = input.x > 0 ? 1 : -1;

        if(ladderMove.CanClimb() && Input.GetAxis("Vertical") != 0 && !ladderMove.OnLadder()) {
            ladderMove.StartClimb();
        }

        if(ladderMove.OnLadder()) {
            ladderMove.Move(input);
            motor.Enable(false);
        }
        else {
            motor.Enable(true);
            motor.Move(input);
        }
    }
}
