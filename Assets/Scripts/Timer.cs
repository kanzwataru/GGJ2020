using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timer;
    public int timerLeft = 99;
    public int endTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LoseTime");
    }

    

    IEnumerator LoseTime()
    {
        while (timerLeft > 0)
        {
            yield return new WaitForSeconds(1);
            timerLeft--;

        }
        
    }

    // Update is called once per frame
    void Update()
    {

        timer.text = ("" + timerLeft);

       
    }


    private void GameFinished()
    {
        if (timer.text == ("95"))
        {
            Debug.Log("Game Finished");
        }
    }


}
