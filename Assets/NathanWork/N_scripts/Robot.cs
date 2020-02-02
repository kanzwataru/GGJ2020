using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public BoxCollider boxCollider;
    public Animator chickenAnimator;

    private int health;
    private RobotAttack robotAttack;
    public bool isBlocking = false;
    public float speed = 1.5f;

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
        var id = GetComponent<RobotAttack>().isPlayerOne ? 0 : 1;
        var gamepad = PlayerManager.players[id].GetGamepad();

        //float inputH = Input.GetAxis("Horizontal");
        float inputH = gamepad.rightStick.x.ReadValue();

        if (!isBlocking)
        {
            gameObject.transform.position = new Vector2(transform.position.x + (inputH * speed * Time.deltaTime), transform.position.y);
        }

        if (inputH != 0)
        {
            chickenAnimator.SetBool("isWalking", true);
        } else
        {
            chickenAnimator.SetBool("isWalking", false);
        }

        //if (Input.GetKeyDown(KeyCode.G) && !isBlocking)
        if (gamepad.crossButton.wasPressedThisFrame && !isBlocking)
        {
            chickenAnimator.SetTrigger("jump");
        }

        //if (Input.GetKey(KeyCode.B) && robotAttack.canAttack)
        if (gamepad.rightShoulder.wasPressedThisFrame && robotAttack.canAttack)
        {
            chickenAnimator.SetBool("isBlocking", true);
            robotAttack.canAttack = false;
            isBlocking = true;
        }

        //if (Input.GetKeyUp(KeyCode.B))
        if(gamepad.rightShoulder.wasReleasedThisFrame)
        {
            chickenAnimator.SetBool("isBlocking", false);
            robotAttack.canAttack = true;
            isBlocking = false;
        }

    }

}
