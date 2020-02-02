using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager {
    public static Player[] players = new Player[2];
}

public class Player : MonoBehaviour
{
    public int playerID = -1;

    public UnityEngine.InputSystem.Gamepad GetGamepad() {
        return UnityEngine.InputSystem.Gamepad.all[playerID];
    }
    // Start is called before the first frame update
    void Start()
    {
        if(playerID == -1) {
            Debug.LogError("SET PLAYER ID!!!");
        }

        PlayerManager.players[playerID] = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
