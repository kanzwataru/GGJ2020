using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public float time;
    public GameObject fade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void NextScene()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        fade.SetActive(true);
        yield return new WaitForSeconds(time);
        
        SceneManager.LoadScene("UIScene");
    }
}
