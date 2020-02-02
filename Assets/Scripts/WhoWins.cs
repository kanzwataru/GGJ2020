using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhoWins : MonoBehaviour
{
    public int player1Health;
    public int player2Health;

    public GameObject player1Wins;
    public GameObject player2Wins;

    public float time;

    void Start()
    {
        PlayerHealthScript.instance.currentHealth = player1Health;
        PlayerHealthScript2.instance.currentHealth = player2Health;
        StartCoroutine(Timer());
        
    }

   IEnumerator Timer()
    {
        if (player1Health > player2Health)
        {
            yield return new WaitForSeconds(time);
            player1Wins.SetActive(true);
        }
        else
        {
            yield return new WaitForSeconds(time);
            player2Wins.SetActive(true);
        }
    }


    
    
}
