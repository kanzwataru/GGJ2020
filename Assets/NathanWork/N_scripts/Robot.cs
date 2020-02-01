using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public BoxCollider boxCollider;

    private int health;

    void Start()
    {
        health = GameLogic.health;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fist")
        {
            print("Hit!");
            health -= GameLogic.punchDamage;
        }
            
    }

}
