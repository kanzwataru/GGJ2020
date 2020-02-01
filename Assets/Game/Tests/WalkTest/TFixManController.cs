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

        velocity = ClampRaycastCollision(velocity);

        var pos = transform.localPosition;
        transform.localPosition = pos + velocity;
    }

    Vector3 ClampRaycastCollision(Vector3 velocity)
    {
        float castDist = 2.0f;
        var castPos = transform.position;
        var nextCastPos = transform.position + transform.TransformDirection(velocity);

        Debug.DrawRay(castPos, transform.TransformDirection(new Vector3(castDist, 0, 0)));
        Debug.DrawRay(castPos, transform.TransformDirection(new Vector3(-castDist, 0, 0)));

        RaycastHit hitInfo;
        if(Physics.Raycast(castPos, transform.TransformDirection(new Vector3(1, 0, 0)), out hitInfo, castDist)) {
            //Debug.Log("see right");
            if(hitInfo.distance < colliderRadius) {
                velocity.x = -((colliderRadius + stopDist) - hitInfo.distance);
                //Debug.Log("penetrating right");
            }
            else if(hitInfo.distance < colliderRadius + stopDist) {
                velocity.x = Mathf.Min(velocity.x, 0);
            }
            else {
                RaycastHit nextHitInfo;
                if(Physics.Raycast(nextCastPos, transform.TransformDirection(new Vector3(1, 0, 0)), out nextHitInfo, castDist)) {
                    if(nextHitInfo.distance < colliderRadius) {
                        velocity.x = Mathf.Min(velocity.x, 0);
                        //Debug.Log("will have penetrated right");
                    }
                }
                else {
                    //Debug.Log("will have gone through right");
                    velocity.x = Mathf.Min(velocity.x, 0);
                }
            }
        }

        if(Physics.Raycast(castPos, transform.TransformDirection(new Vector3(-1, 0, 0)), out hitInfo, castDist)) {
            //Debug.Log("see left");
            if(hitInfo.distance < colliderRadius) {
                velocity.x = ((colliderRadius + stopDist) - hitInfo.distance);
                //Debug.Log("penetrating left");
            }
            else if(hitInfo.distance < colliderRadius + stopDist) {
                velocity.x = Mathf.Max(velocity.x, 0);
            }
            else {
                RaycastHit nextHitInfo;
                if(Physics.Raycast(nextCastPos, transform.TransformDirection(new Vector3(-1, 0, 0)), out nextHitInfo, castDist)) {
                    if(nextHitInfo.distance < colliderRadius) {
                        velocity.x = Mathf.Max(velocity.x, 0);
                        //Debug.Log("will have penetrated right");
                    }
                }
                else {
                    //Debug.Log("will have gone through right");
                    velocity.x = Mathf.Max(velocity.x, 0);
                }
            }
        }

        return velocity;
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
