using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairmanMotor : MonoBehaviour
{
    public float maxSpeed = 1;
    public float accel = 0.5f;
    public float decel = 0.25f;
    public float jumpSpeed = 2;
    public float jumpAccel = 0.95f;
    public float jumpDecel = 0.25f;
    public float jumpTimerLength = 0.15f;
    public float gravity = 0.9f;
    public float stopDist = 0.1f;

    Vector3 dir = Vector3.zero;
    Vector3 inertia = Vector3.zero;

    float jumpTimer;
    bool jumpDone = false;

    float colliderRadius;

    public void Move(Vector2 input) {
        this.dir = new Vector3(input.x, input.y, 0);
    }

    public void ResetInertia() {
        inertia = Vector3.zero;
    }

    public void Enable(bool value) {
        if(enabled != value)
            ResetInertia();
        
        enabled = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        jumpTimer = jumpTimerLength;
        colliderRadius = GetComponent<CapsuleCollider>().radius;
    }

    float CalcInertia(float current, float dir, float accel, float decel) {
        if(dir != 0)
            return Mathf.Clamp(current + (accel * dir), -1.0f, 1.0f);
        else
            return Mathf.Clamp(Mathf.Abs(current) - decel, 0, 1.0f) * Mathf.Sign(current);
    }

    float CalcGravity(float current, float dir, float accel, float decel) {
        if(dir > 0)
            return Mathf.Clamp(current + (accel * dir), 0.0f, 1.0f);
        else
            return Mathf.Clamp(current - decel, -1.0f, 0.0f);
    }

    float CalcJump(float input) {
        float force = 0;

        if(input > 0) {
            if(jumpTimer < jumpTimerLength) {
                jumpTimer += Time.deltaTime;
                force = 1;
            }
        }
        else {
            if(inertia.y == 0) {
                jumpTimer = 0;
            }
            force = 0;
        }

        return force;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        inertia.x = CalcInertia(inertia.x, dir.x, accel, decel);
        inertia.y = CalcGravity(inertia.y, CalcJump(dir.y), jumpAccel, jumpDecel);

        float verticalSpeed = (inertia.y > 0) ? jumpSpeed : gravity;

        var moveDelta = new Vector3(
            inertia.x * (maxSpeed * Time.deltaTime),
            inertia.y * (verticalSpeed * Time.deltaTime),
            0
        );

        moveDelta = ClampRaycastCollision(moveDelta);

        var pos = transform.localPosition;
        pos += moveDelta;
        transform.localPosition = pos;
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

        castPos += new Vector3(0, colliderRadius, 0);
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
}
