﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    None = 0,
    Normal_Attack,
    Elemental_Skill,
    Elemental_Burst,
    Climbing,
    Swimming,
    Die,
    InMenu
}

public class Player : MonoBehaviour
{
    public PlayerState playerState;
    private Element playerElement;
    private PlayerAttack playerAttack;
    public PlayerSkill playerSkill;

    private float playerHp;
    private float playerMaxHp;
    private float playerAtk;
    private float playerGrd;
    private float playerStamina;
    private float playerMaxStamina = 10.0f;

    private bool isInMenu = false;
    public GameObject menu;
    public GameObject ingredientUI;
    public Text txtIngredientName;

    void Awake()
    {
        playerState = PlayerState.None;
        playerAttack = GetComponent<PlayerAttack>();
        playerSkill = GetComponent<PlayerSkill>();
    }

    void Update()
    {
        OnOffPlayerScript();
        GoToMenu();
    }

    void PlayerRayCast()
    {

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
        else if (playerState == PlayerState.Normal_Attack)
        {
            GetComponent<PlayerMove>().enabled = false;
            GetComponent<PlayerAttack>().enabled = true;
            GetComponent<PlayerAnimController>().enabled = true;

            playerAttack.weapon.SetActive(true);
            playerAttack.backWeapon.SetActive(false);
        }
        else if(playerState == PlayerState.Elemental_Skill)
        {
            GetComponent<PlayerMove>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;

            playerAttack.weapon.SetActive(false);
            playerAttack.backWeapon.SetActive(true);
        }
        else if (playerState == PlayerState.Elemental_Burst)
        {
            GetComponent<PlayerMove>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;

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

    void OnTriggerStay(Collider col)
    {
        if(col.CompareTag("Ingredient"))
        {
            txtIngredientName.text = col.name;
            ingredientUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                Destroy(col.gameObject);
                ingredientUI.SetActive(false);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Ingredient"))
        {
            ingredientUI.SetActive(false);
        }
    }
}
