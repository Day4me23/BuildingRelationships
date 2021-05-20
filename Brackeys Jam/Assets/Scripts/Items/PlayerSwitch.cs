using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    public Vector2 player1Pos;
    public Vector2 player2Pos;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("button press");

            GameObject [] players = GameObject.FindGameObjectsWithTag("Player");

            Debug.Log(players.Length);
            
            player1Pos = (Vector2)players[0].transform.position;
            player2Pos = (Vector2)players[1].transform.position;



            players[0].transform.GetComponent<Player>().SetPos(player2Pos);
            players[1].transform.GetComponent<Player>().SetPos(player1Pos);
        }
    }
}