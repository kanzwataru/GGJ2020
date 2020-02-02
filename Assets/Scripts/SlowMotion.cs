using UnityEngine;
using System.Collections;

public class SlowMotion : MonoBehaviour
{

    float currentAmount = 0f;
    float maxAmount = 5f;


    // Update is called once per frame
    void Update()
    {
            if (Time.timeScale == 1.0f)
                Time.timeScale = 0.3f;

            else

                Time.timeScale = 1.0f;
            Time.fixedDeltaTime = 0.05f * Time.timeScale;
        


        if (Time.timeScale == 0.03f)
        {

            currentAmount += Time.deltaTime;
        }

        if (currentAmount > maxAmount)
        {

            currentAmount = 0f;
            Time.timeScale = 1.0f;

        }

    }
}