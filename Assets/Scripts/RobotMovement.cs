using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    public Vector2 moveDirection;
    public float moveSpeed;
    public Rigidbody rb;

    public Animator anim;

    public float deceleration;
    public float jumpSpeed;

    public bool isJumping = false;
    public bool isGrounded = true;


    

    // Update is called once per frame
    void Update()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        //moveDirection.y = Input.GetAxisRaw("Vertical");

        rb.velocity = moveDirection * moveSpeed;


        if (Input.GetKeyDown(KeyCode.Z) && isGrounded)
        {
            //anim.SetTrigger("isPunching");
            anim.SetBool("isJumping", true);
            rb.AddForce(transform.up * 100, ForceMode.Impulse);
            isJumping = true;
            isGrounded = false;
            
            StartCoroutine(grounded());
            
        }

        



        /*if (Input.GetKeyUp(KeyCode.Z))
        {
            anim.SetBool("isJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            //jump
            Debug.Log("Jump");
            rb.AddForce(transform.forward * 1000, ForceMode.Impulse);
            /*rb.AddForce(0, jumpSpeed, 0, ForceMode.Impulse);
               //moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);*/

    IEnumerator grounded()
        {
            Debug.Log("grounded");
            yield return new WaitForSeconds(1f);
            isJumping = false;
            isGrounded = true;
        }

    }


    public void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Physics.gravity * 20f, ForceMode.Acceleration);
    }

}

