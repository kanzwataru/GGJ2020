using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairRing : MonoBehaviour
{
    public Transform playerRing;
    public Transform goalRing;
    public SpriteRenderer nice;
    public SpriteRenderer bad;
    public int times = 3;
    public float ringSpeed = 3f;

    private float actualRingSpeed;
    private float difficulty = 0.03f;
    private Vector2 initialScale;
    private bool canPlay = true;
    private int successCounter;

    // Press SPACE when player ring is close to goal ring x times to win

    void Start()
    {
        initialScale = playerRing.localScale;
        actualRingSpeed = ringSpeed * 0.001f;
    }

    void succeed()
    {
        // grant player benefits
        print("You won the repair game");
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
