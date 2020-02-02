using UnityEngine;
using System.Collections;

public class SlowMotion : MonoBehaviour
{

    float currentAmount = 0f;
    float maxAmount = 3f;


    // Update is called once per frame
    void Update()
    { 
            if (Time.timeScale <= 1.0f)
                Time.timeScale = 0.2f;

            else

                Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.05f * Time.timeScale;
        


        if (Time.timeScale <= 0.03f)
        {

            currentAmount ++;
        }

        if (currentAmount > maxAmount)
        {

            currentAmount = 0f;
            Time.timeScale = 1.0f;

        }

    }
}