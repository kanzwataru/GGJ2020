using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public BoxCollider boxCollider;
    public Animator chickenAnimator;

    private int health;
    [HideInInspector] public RobotAttack robotAttack;
    public bool isBlocking = false;
    public bool canWalk = true;
    public float speed = 1.5f;
    private float inputH;


    void Start()
    {
        health = GameLogic.health;
        robotAttack = this.gameObject.GetComponent<RobotAttack>();
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
        if (!isBlocking && canWalk)
        {
            inputH = Input.GetAxis("Horizontal");
            gameObject.transform.position = new Vector2(transform.position.x + (inputH * speed * Time.deltaTime), transform.position.y);
        } else
        {
            inputH = 0;
        }

        if (inputH != 0)
        {
            chickenAnimator.SetBool("isWalking", true);
        } else
        {
            chickenAnimator.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.G) && !isBlocking)
        {
            chickenAnimator.SetTrigger("jump");
        }

        if (Input.GetKey(KeyCode.B) && robotAttack.canAttack)
        {
            chickenAnimator.SetBool("isBlocking", true);
            robotAttack.canAttack = false;
            isBlocking = true;
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            chickenAnimator.SetBool("isBlocking", false);
            robotAttack.canAttack = true;
            isBlocking = false;
        }

    }

}
