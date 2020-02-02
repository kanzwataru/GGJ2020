﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAttack : MonoBehaviour
{
    //Punch
    //Missiles

    public int punchDamage = 3;
    public bool isPlayerOne = true;
    public Animator robotAnimator;

    public RoomManager roomManager;
    public bool canAttack = true;
    public bool canPunch = true;
    private int numberOfRooms;
    private int currentTarget = 0;


    void Start()
    {
        robotAnimator = robotAnimator.GetComponent<Animator>();
        roomManager = roomManager.GetComponent<RoomManager>();
        numberOfRooms = roomManager.rooms.Length;
        //GameLogic.punchRoom.AddListener(punchState);
    }

    void punchState(bool isFixed)
    {
        canPunch = isFixed;
    }

    void Update()
    {
        //punch
        if (Input.GetKeyDown("space") && canAttack && canPunch)
        {
            robotAnimator.SetTrigger("punch"); 
            //has a frame with a boxcollider set ontrigger for tag "Fist"
        }

        //change target
        if (Input.GetKeyDown("x") && canAttack)
        {
            roomManager.rooms[currentTarget].target.enabled = false;
            currentTarget = (currentTarget + 1) % numberOfRooms;
            for (int i = 0; i < numberOfRooms; i++)
            {
                if (!roomManager.rooms[currentTarget].roomHit)
                {
                    roomManager.rooms[currentTarget].target.enabled = true;
                    return;
                } else
                {
                    currentTarget = (currentTarget + 1) % numberOfRooms;
                }
            }
            //Edge case: all rooms hit
            //--> do nothing
        }

        //fire missile
        if (Input.GetKeyDown("c") && canAttack)
        {
            //check if missile silo is full
            //start a cooldown
            //animation
            roomManager.rooms[currentTarget].roomHit = true;
            roomManager.rooms[currentTarget].target.enabled = false;
            roomManager.rooms[currentTarget].sparkMachine.SetBool("sparking", true);
            roomManager.rooms[currentTarget].explosionMachine.SetTrigger("explode");
            roomManager.rooms[currentTarget].explosionMachine.SetBool("exploding", true);

            //problems
            switch (roomManager.rooms[currentTarget].roomType)
            {
                case RoomType.heal:
                    print("healing broken");
                    break;
                case RoomType.walk:
                    print("walking broken");
                    //robot.canWalk = true; //get enemy robot
                    //signal dispatch
                    //enemy robot hit
                    break;
                case RoomType.ammo:
                    print("ammo broken");

                    break;
                case RoomType.punch:
                    print("punch broken");
                    //robot.robotAttack.canAttack = true;
                    //GameLogic.punchRoom.Invoke();
                    break;
                case RoomType.block:
                    print("block broken");

                    break;
                case RoomType.rockets:
                    print("rockets broken");

                    break;
            }
        }
    }
}
