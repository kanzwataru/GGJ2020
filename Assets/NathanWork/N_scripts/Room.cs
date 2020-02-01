using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector] public BoxCollider roomCollider;
    [HideInInspector] public SpriteRenderer target;
    public GameObject repairGame;
    public bool roomHit = false;

    private void Start()
    {
        roomCollider = GetComponent<BoxCollider>();
        target = GetComponentInChildren<SpriteRenderer>();
    }


    private void OnTriggerEnter(Collider other)
    {
        
    }

}
