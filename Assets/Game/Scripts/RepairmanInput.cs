using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RepairmanInput : MonoBehaviour
{
    RepairmanMotor motor;
    RepairmanFix fix;
    BetterLadderMovement ladderMove;

    public int facingDir = 1;

    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<RepairmanMotor>();
        ladderMove = GetComponent<BetterLadderMovement>();
        fix = GetComponent<RepairmanFix>();
    }

    // Update is called once per frame
    void Update()
    {
        var gamepad = GetComponent<Player>().GetGamepad();

        var input = new Vector3(
            gamepad.leftStick.x.ReadValue(),
            gamepad.leftStick.y.ReadValue(),
            0
        );

        if(gamepad.leftTrigger.ReadValue() > 0) {
            fix.Fix();
        }
        else {
            fix.StopFixing();
        }

        if(input.x != 0)
            facingDir = input.x > 0 ? 1 : -1;

        if(ladderMove.CanClimb() && (input.y > 0.2f || input.y < -0.2f) && !ladderMove.OnLadder()) {
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
