using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    None,
    Attaking,
    Swimming,
    Die,
    InMenu
}

public class Player : MonoBehaviour
{
    public PlayerState playerState;
    private PlayerAttack playerAttack;

    private float hp;
    private float atk;
    private float grd;

    private bool isInMenu = false;
    public GameObject menu;

    void Start()
    {
        playerState = PlayerState.None;
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        OnOffPlayerScript();
        GoToMenu();
    }

    void OnOffPlayerScript()
    {
        if (playerState == PlayerState.None)
        {
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<PlayerMove>().enabled = true;
            GetComponent<PlayerAnimController>().enabled = true;

            playerAttack.weapon.SetActive(false);
            playerAttack.backWeapon.SetActive(true);
        }
        else if (playerState == PlayerState.Attaking)
        {
            GetComponent<PlayerMove>().enabled = false;
            GetComponent<PlayerAttack>().enabled = true;
            GetComponent<PlayerAnimController>().enabled = true;

            playerAttack.weapon.SetActive(true);
            playerAttack.backWeapon.SetActive(false);
        }
        else if (playerState == PlayerState.InMenu)
        {
            GetComponent<PlayerMove>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            //GetComponent<PlayerAnimController>().enabled = false;
        }
    }

    void GoToMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isInMenu = isInMenu ? false : true;

            if (isInMenu)
            {
                playerState = PlayerState.InMenu;
                menu.SetActive(true);
            }
            else
            {
                playerState = PlayerState.None;
                menu.SetActive(false);
            }
        }
    }
}
