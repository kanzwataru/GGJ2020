﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBrokenPieces : MonoBehaviour
{
    public GameObject[] brokenPieces;
    public int maxPieces = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);

            int piecesToDrop = Random.Range(1, maxPieces);
            for(int i= 0; i < piecesToDrop; i++)
            {
                int randomPiece = Random.Range(0, brokenPieces.Length);

                Instantiate(brokenPieces[randomPiece], transform.position, transform.rotation);
            }
        }
    }
}
