using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RepairRing : MonoBehaviour
{
    public Transform playerRing;
    public Transform goalRing;
    public SpriteRenderer nice;
    public SpriteRenderer bad;
    public int times = 3;
    public float ringSpeed = 3f;
    public float difficulty = 0.05f;

    private float actualRingSpeed;
    private Vector2 initialScale;
    private Vector2 initialPos;
    private bool canPlay = true;
    private int successCounter;

    public UnityEvent success;

    // Press SPACE when player ring is close to goal ring x times to win

    void Start()
    {
        if (success == null)
            success = new UnityEvent();

        initialScale = playerRing.localScale;
        initialPos = this.gameObject.transform.position;
        actualRingSpeed = ringSpeed * 0.001f;
    }

    public void resetPosition()
    {
        this.gameObject.transform.position = initialPos;
    }

    void succeed()
    {
        // grant player benefits
        print("You won the repair game");
        resetPosition();
        success.Invoke();
    }

    IEnumerator reloadSuccess(bool final)
    {
        yield return new WaitForSeconds(0.2f);
        nice.enabled = true;
        yield return new WaitForSeconds(0.5f);
        nice.enabled = false;
        yield return new WaitForSeconds(0.1f);
        if (final)
        {
            succeed();
        } else
        {
            canPlay = true;
            playerRing.localScale = initialScale;
        }
    }

    IEnumerator reloadFail()
    {
        yield return new WaitForSeconds(0.2f);
        bad.enabled = true;
        yield return new WaitForSeconds(0.5f);
        bad.enabled = false;
        yield return new WaitForSeconds(0.1f);
        canPlay = true;
        playerRing.localScale = initialScale;
    }

    void Update()
    {
        Vector2 scale = playerRing.localScale;

        if (canPlay)
        {
            if (Input.GetKeyDown("space"))
            {
                canPlay = false;
                playerRing.localScale = Vector2.zero;
                Vector2 goalScale = goalRing.localScale;
                if (scale.x < goalScale.x + difficulty && scale.x > goalScale.x - difficulty)
                {
                    successCounter++;
                    if (successCounter >= times)
                    {
                        StartCoroutine(reloadSuccess(true));
                    }
                    else
                    {
                        StartCoroutine(reloadSuccess(false));
                    }
                }
                else //not good try again
                {
                    successCounter = 0;
                    StartCoroutine(reloadFail());
                }
            }
            else
            {
                if (scale.x <= 0 || scale.y <= 0)
                {
                    canPlay = false;
                    successCounter = 0;
                    StartCoroutine(reloadFail());

                } else
                {
                    playerRing.localScale = new Vector2(scale.x - actualRingSpeed, scale.y - actualRingSpeed);
                }
            }
        }
    }
}
