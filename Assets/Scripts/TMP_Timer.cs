﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TMP_Timer : MonoBehaviour
{
	public TMP_Text timer;
	public int timerLeft = 99;
	public int endTime = 0;
    public Animator anim;
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

        if (timerLeft == 10)
        {
            anim.SetBool("timeEnd", true);
        }
    }


	private void GameFinished()
	{
		if (timer.text == ("90"))
		{
			Debug.Log("Game Finished");
            anim.SetBool("timeEnd", false);
		}
	}


}
