using UnityEngine;
using System.Collections;

// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class JumpTest : MonoBehaviour
{
    public AnimationCurve animCurve;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;


    public Vector2 moveDirection;

    public Rigidbody rb;

    public bool isJumping = false;
    public bool isGrounded;

    public float graphValue;


    void Update()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
   

        rb.velocity = moveDirection * speed;

       

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            Debug.Log("Jumped");
            isJumping = true;
            isGrounded = false;
            GetComponent<Rigidbody>().AddForce(Vector2.up * jumpSpeed, ForceMode.Acceleration);
            StartCoroutine(JumpFreeze());

        }


        IEnumerator JumpFreeze()
        {
            yield return new WaitForSeconds(0.05f);
            rb.constraints = RigidbodyConstraints.FreezeAll;
            yield return new WaitForSeconds(0.5f);
            rb.constraints &= ~RigidbodyConstraints.FreezePosition;
        }

        graphValue = animCurve.Evaluate(Time.time);

       
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Floor")
        {
            isGrounded = true;
            isJumping = false;
        }
    }




    public void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Physics.gravity * 20f, ForceMode.Acceleration);
    }
}