using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TFixManController : MonoBehaviour
{
    public float speed = 10.0f;
    public float stopDist = 0.05f;

    public Transform parentRoom;

    float colliderRadius;

    // Start is called before the first frame update
    void Start()
    {
        colliderRadius = GetComponent<CapsuleCollider>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        var input = new Vector3(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"),
            0.0f
        );

        //var dir = parentRoom.TransformDirection(input);
        var dir = input;
        var velocity = dir * speed * Time.deltaTime;

        if(transform.position.z != 0) {
            velocity.z = (0 - transform.position.z) * 0.05f;
        }

        //var comp = GetComponent<CharacterController>();
        //comp.Move(velocity);
        /*
        var pos = transform.position;
        transform.position = pos + velocity;
        */

        float castDist = 2.0f;

        Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(castDist, 0, 0)));
        Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(-castDist, 0, 0)));

        RaycastHit hitInfo;
        if(Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(1, 0, 0)), out hitInfo, castDist)) {
            Debug.Log("see right");
            if(hitInfo.distance < colliderRadius) {
                velocity.x = -((colliderRadius + stopDist) - hitInfo.distance);
                Debug.Log("penetrating right");
            }
            else if(hitInfo.distance < colliderRadius + stopDist) {
                velocity.x = Mathf.Min(velocity.x, 0);
            }
        }

        if(Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-1, 0, 0)), out hitInfo, castDist)) {
            Debug.Log("see left");
            if(hitInfo.distance < colliderRadius) {
                velocity.x = ((colliderRadius + stopDist) - hitInfo.distance);
                Debug.Log("penetrating left");
            }
            else if(hitInfo.distance < colliderRadius + stopDist) {
                velocity.x = Mathf.Max(velocity.x, 0);
            }
        }

        var pos = transform.localPosition;
        transform.localPosition = pos + velocity;
    }

    void OnCollisionStay(Collision collision)
    {
        return;
        Debug.Log("hit");

        var pos = transform.position;
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.Log(contact.separation);
            pos += contact.normal * contact.separation;
            //Debug.DrawRay(contact.point, contact.normal, Color.white);
        }

        transform.position = pos;
    }
}
