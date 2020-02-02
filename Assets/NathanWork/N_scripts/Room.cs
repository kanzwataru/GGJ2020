using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector] public BoxCollider roomCollider;
    [HideInInspector] public SpriteRenderer target;
    public Transform repairGuide;
    private GameObject repairGame;
    private bool inRoom = false;

    public bool roomHit = false;

    private void Start()
    {
        roomCollider = GetComponent<BoxCollider>();
        target = GetComponentInChildren<SpriteRenderer>();
        repairGuide = repairGuide.GetComponent<Transform>();
        repairGame = GameObject.Find("RepairGame");
        repairGame.GetComponent<RepairRing>().success.AddListener(roomFixed);
    }

    private void roomFixed()
    {
        // only if still in room
        if (inRoom)
        {
            roomHit = false;
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
