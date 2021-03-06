﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TFixManController : MonoBehaviour
{
    public float speed = 10.0f;
    public float stopDist = 0.05f;
    public float jumpTimerLength = 1.0f;
    public float jumpSpeed = 15.0f;
    public float fallSpeed = 1.5f;
    public float gravity = 2.0f;

    public Transform parentRoom;

    float jumpTimer = 0.0f;
    bool jumpDone = false;
    Vector3 inertia;


    float colliderRadius;

    // Start is called before the first frame update
    void Start()
    {
        colliderRadius = GetComponent<CapsuleCollider>().radius;
    }

    float JumpForce(float inputForce)
    {
        float force = 0.0f;
        bool jump = inputForce > 0;

        if(!jump) {
            jumpDone = false;
        }

        if(jumpTimer < jumpTimerLength) {
            if(jump && !jumpDone && inertia.y >= 0) {
                jumpTimer += Time.deltaTime;
                force = jumpSpeed;
            }
            else {
                jumpTimer = 0;
                force = 0;
            }
        }
        else {
            jumpTimer = 0;
            jumpDone = true;
        }

        return force;
    }

    Vector3 CalcInertia(Vector3 inertia, Vector3 impulse, float gravity)
    {
        inertia.y -= gravity * Time.deltaTime;
        inertia.y = Mathf.Max(inertia.y, -fallSpeed);
        
        inertia.x = Mathf.Lerp(inertia.x, 0, 0.1f * Time.deltaTime);
        inertia += impulse * Time.deltaTime;

        return inertia;
    }

    void FixedUpdate()
    {
        var input = new Vector3(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"),
            0.0f
        );

        var impulse = new Vector3(
            input.x * speed,
            JumpForce(input.y),
            0.0f
        );

        //var dir = parentRoom.TransformDirection(input);
        inertia = CalcInertia(inertia, impulse, gravity);

        var velocity = inertia;

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
        Debug.DrawRay(castPos, transform.TransformDirection(new Vector3(0, -castDist, 0)));

        RaycastHit hitInfo;
        if(Physics.Raycast(castPos, transform.TransformDirection(new Vector3(1, 0, 0)), out hitInfo, castDist)) {
            //Debug.Log("see right");
            if(hitInfo.distance < colliderRadius) {
                velocity.x = -((colliderRadius + stopDist) - hitInfo.distance);
                inertia.x = 0;
                //Debug.Log("penetrating right");
            }
            else if(hitInfo.distance < colliderRadius + stopDist) {
                velocity.x = Mathf.Min(velocity.x, 0);
                inertia.x = 0;
            }
            else {
                RaycastHit nextHitInfo;
                if(Physics.Raycast(nextCastPos, transform.TransformDirection(new Vector3(1, 0, 0)), out nextHitInfo, castDist)) {
                    if(nextHitInfo.distance < colliderRadius) {
                        velocity.x = Mathf.Min(velocity.x, 0);
                        inertia.x = 0;
                        //Debug.Log("will have penetrated right");
                    }
                }
                else {
                    //Debug.Log("will have gone through right");
                    velocity.x = Mathf.Min(velocity.x, 0);
                    inertia.x = 0;
                }
            }
        }

        if(Physics.Raycast(castPos, transform.TransformDirection(new Vector3(-1, 0, 0)), out hitInfo, castDist)) {
            //Debug.Log("see left");
            if(hitInfo.distance < colliderRadius) {
                velocity.x = ((colliderRadius + stopDist) - hitInfo.distance);
                inertia.x = 0;
                //Debug.Log("penetrating left");
            }
            else if(hitInfo.distance < colliderRadius + stopDist) {
                velocity.x = Mathf.Max(velocity.x, 0);
                inertia.x = 0;
            }
            else {
                RaycastHit nextHitInfo;
                if(Physics.Raycast(nextCastPos, transform.TransformDirection(new Vector3(-1, 0, 0)), out nextHitInfo, castDist)) {
                    if(nextHitInfo.distance < colliderRadius) {
                        velocity.x = Mathf.Max(velocity.x, 0);
                        inertia.x = 0;
                        //Debug.Log("will have penetrated right");
                    }
                }
                else {
                    //Debug.Log("will have gone through right");
                    velocity.x = Mathf.Max(velocity.x, 0);
                    inertia.x = 0;
                }
            }
        }

        if(Physics.Raycast(castPos, transform.TransformDirection(new Vector3(0, -1, 0)), out hitInfo, castDist)) {
            //Debug.Log("see left");
            if(hitInfo.distance < colliderRadius) {
                velocity.y = ((colliderRadius + stopDist) - hitInfo.distance);
                inertia.y = 0;
                //Debug.Log("penetrating left");
            }
            else if(hitInfo.distance < colliderRadius + stopDist) {
                velocity.y = Mathf.Max(velocity.y, 0);
                inertia.y = 0;
            }
            else {
                RaycastHit nextHitInfo;
                if(Physics.Raycast(nextCastPos, transform.TransformDirection(new Vector3(0, -1, 0)), out nextHitInfo, castDist)) {
                    if(nextHitInfo.distance < colliderRadius) {
                        velocity.y = Mathf.Max(velocity.y, 0);
                        inertia.y = 0;
                        //Debug.Log("will have penetrated right");
                    }
                }
                else {
                    //Debug.Log("will have gone through right");
                    velocity.y = Mathf.Max(velocity.y, 0);
                    inertia.y = 0;
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
