using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    None,
    Attaking,
    Swimming,
    Die
}

public class Player : MonoBehaviour
{
    public PlayerState playerState;

    private float hp;
    private float atk;
    private float grd;

    void Start()
    {

    }

    void Update()
    {
        if (playerState == PlayerState.Attaking)
        {
            GetComponent<PlayerMove>().enabled = false;
            GetComponent<PlayerAttack>().enabled = true;
        }
        else if (playerState == PlayerState.None)
        {
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<PlayerMove>().enabled = true;
        }
    }
}
