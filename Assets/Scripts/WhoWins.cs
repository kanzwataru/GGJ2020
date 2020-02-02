using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhoWins : MonoBehaviour
{
    public bool player1;


    public GameObject player2Win;

    private void Start()
    {
        
    }

    public void Update()
    {
        PlayerHealthScript.instance.player1Dead = player1;

        if (player1 == true)
        {
            player2Win.SetActive(true);
        }
    }




    /*public int player1Health;
    public int player2Health;

    public GameObject player1Wins;
    public GameObject player2Wins;

    public float time;
    public bool player1 = false;
    public bool player2 = false;








    void Start()
    {
        PlayerHealthScript.instance.currentHealth = player1Health;
        PlayerHealthScript2.instance.currentHealth = player2Health;

        StartCoroutine(Timer());

    }


    void Update()
    {
        

        if (player1Health < 0.1f)
        {
            player2 = true;
        }
        if(player2Health <0.1f)
        {
            player1 = true;
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);
        if (player1 == true)
        {
            player1Wins.SetActive(true);
        }
        else
        {
            player2Wins.SetActive(true);
        }
        
    }

    


    */

}
