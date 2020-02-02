using UnityEngine;
using System.Collections;


public class JumpTest2 : MonoBehaviour
{


    public float velocity = 3f;
    public float jumpHeight = 2f;
    public float jumpTime = 1f;
    public AnimationCurve jumpCurve;
    //height
    public Vector2 moveDirection;

    Vector3 gravity = new Vector3(0f, -10f, 0f);
    CharacterController controller;
    float time = 0f;
    



    void Start()
    {

        this.controller = GetComponent<CharacterController>();

    }

    Vector3 Gravity
    {

        get
        {

            Ray ray = new Ray();
            ray.origin = this.transform.position;
            ray.direction = Vector3.down;

            //On floor
            if (Physics.Raycast(ray, this.controller.height * 0.55f))
            {

                //Jump
                if (Input.GetKeyDown(KeyCode.Space)) this.time = this.jumpTime;

            }

            //Jump time
            if (this.time > 0f)
            {

                float height = this.jumpCurve.Evaluate((this.jumpTime - this.time) / this.jumpTime);
                this.time -= Time.deltaTime;
                return (Vector3.up
                        * (this.jumpCurve.Evaluate((this.jumpTime - this.time) / this.jumpTime) - height)
                        * this.jumpHeight);

            }

            return this.gravity * Time.deltaTime;

        }

    }

    void Update()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");


        
        this.transform.Translate(0f, Input.GetAxis("Horizontal") * this.velocity, 0f);

        controller.Move(Gravity
                        + this.transform.forward
                        * Input.GetAxis("Vertical")
                        * this.velocity
                        * Time.deltaTime);
                        

    }
}