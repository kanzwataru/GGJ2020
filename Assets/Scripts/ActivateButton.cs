using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButton : MonoBehaviour
{
    public GameObject button;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }


    IEnumerator Timer()
    {
        yield return new WaitForSeconds(time);
        button.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
