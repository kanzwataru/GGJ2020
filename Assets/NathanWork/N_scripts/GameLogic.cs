using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameLogic : MonoBehaviour
{
    private static GameLogic _instance;

    public static GameLogic Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public static int punchDamage = 3;
    public static int health = 10;

    public static UnityEvent<bool> punchRoom;
    public static UnityEvent healRoom;

}
