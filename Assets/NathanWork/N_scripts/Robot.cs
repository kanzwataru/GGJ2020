using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public BoxCollider boxCollider;
    public Animator chickenAnimator;

    private int health;
    public float speed = 1.5f;

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

    private void Update()
    {
        float inputH = Input.GetAxis("Horizontal");

        gameObject.transform.position = new Vector2(transform.position.x + (inputH * speed * Time.deltaTime), transform.position.y);

        if (inputH != 0)
        {
            chickenAnimator.SetBool("isWalking", true);
        } else
        {
            chickenAnimator.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            chickenAnimator.SetTrigger("jump");
        }

    }

}
