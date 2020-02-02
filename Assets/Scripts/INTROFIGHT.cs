using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INTROFIGHT : MonoBehaviour
{
    public float t_zero;
    public GameObject ONE;
    public float t_one;
    public GameObject TWO;
    public float t_two;
    public GameObject FIGHT;
    public float t_fight;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());

    }


    IEnumerator Timer()
    {
        yield return new WaitForSeconds(t_zero);
        ONE.SetActive(true);
        yield return new WaitForSeconds(t_one);
        ONE.SetActive(false);
        TWO.SetActive(true);
        yield return new WaitForSeconds(t_two);
        TWO.SetActive(false);
        FIGHT.SetActive(true);
        yield return new WaitForSeconds(t_fight);
        FIGHT.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
