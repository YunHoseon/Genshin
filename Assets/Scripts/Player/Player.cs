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
    private PlayerAttack playerAttack;

    private float hp;
    private float atk;
    private float grd;

    void Start()
    {
        playerState = PlayerState.None;
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        if (playerState == PlayerState.None)
        {
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<PlayerMove>().enabled = true;

            playerAttack.weapon.SetActive(false);
            playerAttack.backWeapon.SetActive(true);
        }
        else if (playerState == PlayerState.Attaking)
        {
            GetComponent<PlayerMove>().enabled = false;
            GetComponent<PlayerAttack>().enabled = true;

            playerAttack.weapon.SetActive(true);
            playerAttack.backWeapon.SetActive(false);
        }
    }
}
