using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterLadderMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    enum EState {
        NoLadder,
        CanClimb,
        OnLadder
    };

    EState state = EState.NoLadder;
    Ladder currentLadder = null;
    Vector3 dir = Vector3.zero;

    float colliderRadius;
    float colliderHeight;

    public bool CanClimb()
    {
        if(state == EState.CanClimb)
            Debug.Assert(currentLadder);

        return state == EState.CanClimb;
    }

    public bool OnLadder()
    {
        return state == EState.OnLadder;
    }

    public void StartClimb()
    {
        Debug.Log("start climb");
        Debug.Assert(currentLadder);
        Debug.Assert(state == EState.CanClimb);

        if(currentLadder != null) {
            var pos = transform.localPosition;
            pos.x = currentLadder.GetPosition().x;
            transform.localPosition = pos;

            state = EState.OnLadder;
        }
    }

    public void StopClimb()
    {
        Debug.Assert(OnLadder());
        if(currentLadder) {
            state = EState.CanClimb;
        }
        else {
            state = EState.NoLadder;
        }
    }

    public void Move(Vector2 input)
    {
        dir = new Vector3(input.x, input.y, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        colliderRadius = GetComponent<CapsuleCollider>().radius;
        colliderHeight = GetComponent<CapsuleCollider>().height;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!OnLadder())
            return;

        if(OnLadder() && !currentLadder) {
            Debug.Log("On ladder but no ladder!!!, this shouldn't happen");
            StopClimb();
        }
        
        var pos = transform.localPosition;

        if((dir.x > 0.3f || dir.x < -0.3f) && dir.y < 0.3f) {
            StopClimb();
        }

        pos.y += dir.y * moveSpeed * Time.deltaTime;

        if(dir.y < 0 && pos.y < currentLadder.GetBottom().y)
            return;
        else if(dir.y > 0 && pos.y + colliderHeight > currentLadder.GetTop().y)
            return;

        transform.localPosition = pos;
    }

    void OnTriggerEnter(Collider other)
    {
        Ladder ladder = other.GetComponent<Ladder>();
        if(ladder == null)
            return;
        
        currentLadder = ladder;

        switch(state) {
        case EState.NoLadder:
            state = EState.CanClimb;
            break;
        case EState.CanClimb:
            break;
        case EState.OnLadder:
            Debug.Log("Entered another ladder???!!!");
            break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Ladder ladder = other.GetComponent<Ladder>();
        if(ladder == null)
            return;
        
        currentLadder = null;

        switch(state) {
        case EState.NoLadder:
            break;
        case EState.CanClimb:
            state = EState.NoLadder;
            break;
        case EState.OnLadder:
            StopClimb();
            break;
        }
    }
}
