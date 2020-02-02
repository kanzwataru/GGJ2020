using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    heal,
    walk,
    ammo,
    punch,
    block,
    rockets
}

public class Room : MonoBehaviour
{
    [HideInInspector] public BoxCollider roomCollider;
    [HideInInspector] public SpriteRenderer target;
    [HideInInspector] public Animator sparkMachine;
    [HideInInspector] public Animator explosionMachine;

    private Transform repairGuide;
    private GameObject repairGame;
    private bool inRoom = false;
    public bool roomHit = false;
    public RoomType roomType;

    private void Start()
    {
        roomCollider = GetComponent<BoxCollider>();

        target = GetComponentInChildren<SpriteRenderer>();
        repairGuide = this.gameObject.transform.Find("RepairGuide");
        sparkMachine = this.gameObject.transform.Find("SparkMachine").GetComponent<Animator>();
        explosionMachine = this.gameObject.transform.Find("ExplosionMachine").GetComponent<Animator>();
        repairGame = GameObject.Find("RepairGame");
        repairGame.GetComponent<RepairRing>().success.AddListener(roomFixed);
    }

    private void roomFixed()
    {
        // only if still in room
        if (inRoom)
        {
            roomHit = false;
            sparkMachine.SetBool("sparking", false);
            explosionMachine.SetBool("exploding", false);

            switch (roomType)
            {
                case RoomType.heal:
                    print("healing fixed");
                    break;
                case RoomType.walk:
                    print("walking fixed");

                    break;
                case RoomType.ammo:
                    print("ammo fixed");

                    break;
                case RoomType.punch:
                    print("punch fixed");
                    GameLogic.punchRoom.Invoke(true);

                    break;
                case RoomType.block:
                    print("block fixed");

                    break;
                case RoomType.rockets:
                    print("rockets fixed");

                    break;
           }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Entered!");
            inRoom = true;
            if (roomHit)
            {
                print("Repair Game...");
                repairGame.transform.position = repairGuide.position;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Left");
            inRoom = false;
            repairGame.GetComponent<RepairRing>().resetPosition();
        }
    }

}
