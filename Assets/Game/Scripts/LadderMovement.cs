using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float moveOffTimer = 0.5f;

    enum ELadderClimbState {
        NoLadder,
        CanClimb,
        OnLadderTop,
        OnLadder,
        OnLadderBottom
    }

    ELadderClimbState state = ELadderClimbState.NoLadder;
    LadderTrigger currentTrigger = null;
    Vector3 dir = Vector3.zero;

    public bool CanClimb()
    {
        return (currentTrigger != null);
    }

    public bool OnLadder()
    {
        return (state == ELadderClimbState.OnLadder ||
                state == ELadderClimbState.OnLadderTop ||
                state == ELadderClimbState.OnLadderBottom);
    }

    public void StartClimb()
    {
        Debug.Log("start climb");
        if(currentTrigger != null) {
            Debug.Assert(state == ELadderClimbState.CanClimb);
            if(currentTrigger.isTop) {
                state = ELadderClimbState.OnLadderTop;
            }
            else {
                state = ELadderClimbState.OnLadderBottom;
            }

            transform.position = currentTrigger.transform.position;
        }
    }

    public void StopClimb()
    {
        if(OnLadder()) {
            if(currentTrigger == null) {
                state = ELadderClimbState.NoLadder;
            }
            else {
                state = ELadderClimbState.CanClimb;
            }
        }
    }

    public void Move(Vector2 input)
    {
        dir = new Vector3(input.x, input.y, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!OnLadder())
            return;

        var pos = transform.localPosition;
        pos.y += dir.y * moveSpeed * Time.deltaTime;

        if(dir.x != 0) {
            if(!currentTrigger || (currentTrigger && !currentTrigger.throughFloor))
            StopClimb();
        }

        if(currentTrigger && !currentTrigger.throughFloor) {
            if(currentTrigger.isTop) {
                
            }
            else {
                //var ladderPoint = transform.InverseTransformPoint(currentTrigger.transform.position);
                var ladderPoint = currentTrigger.transform.position;
                Debug.Log(ladderPoint.ToString() + " " + pos.ToString());
                pos.y = Mathf.Max(ladderPoint.y, pos.y);
            }
            //Mathf.Clamp(pos.y, )
        }

        transform.localPosition = pos;
    }

    void OnTriggerEnter(Collider other)
    {
        LadderTrigger trigger = other.GetComponent<LadderTrigger>();
        if(trigger == null)
            return;
        
        currentTrigger = trigger;
        if(state == ELadderClimbState.NoLadder)
            state = ELadderClimbState.CanClimb;
    }

    void OnTriggerExit(Collider other)
    {
        LadderTrigger trigger = other.GetComponent<LadderTrigger>();
        if(trigger == null)
            return;

        currentTrigger = null;
        if(state == ELadderClimbState.CanClimb)
            state = ELadderClimbState.NoLadder;
    }
}
