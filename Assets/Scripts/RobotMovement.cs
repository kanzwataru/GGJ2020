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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        //moveDirection.y = Input.GetAxisRaw("Vertical");

        rb.velocity = moveDirection * moveSpeed;


        if (Input.GetKeyDown(KeyCode.Z))
        {
            //anim.SetTrigger("isPunching");
            anim.SetBool("isRealPunching", true);
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            anim.SetBool("isRealPunching", false);
        }


        if (Input.GetKey(KeyCode.X))
        {
            //jump
            Debug.Log("Jump");
            /*rb.AddForce(0, jumpSpeed, 0, ForceMode.Impulse);
               //moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);*/



        }

    }
}
