using System.Collections;
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
    public bool canAttack = false;
    private int numberOfRooms;
    private int currentTarget = 0;

    void Start()
    {
        robotAnimator = robotAnimator.GetComponent<Animator>();
        roomManager = roomManager.GetComponent<RoomManager>();
        numberOfRooms = roomManager.rooms.Length;
    }

    void Update()
    {
        //punch
        if (Input.GetKeyDown("space") && canAttack)
        {
            robotAnimator.SetTrigger("punch"); 
            //has a frame with a boxcollider set ontrigger for tag "Fist"
        }

        //change target
        if (Input.GetKeyDown("x"))
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
        }
    }
}
