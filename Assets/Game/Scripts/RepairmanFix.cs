using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairmanFix : MonoBehaviour
{
    bool fixing = false;

    public bool IsFixing() {
        return fixing;
    }

    public void Fix() {
        fixing = true;
    }

    public void StopFixing() {
        fixing = false;
    }

    public void Update()
    {
        if(fixing) {
            // do fixing stuff here
        }
    }
}
