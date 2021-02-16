using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    None = 0,
    Attaking,
    Skill1,
    Skill2,
    Swimming,
    Die,
    InMenu
}

public class Player : MonoBehaviour
{
    public PlayerState playerState;
    private Element playerElement;
    private PlayerAttack playerAttack;

    private float playerHp;
    private float playerMaxHp;
    private float playerAtk;
    private float playerGrd;
    private float playerStamina;
    private float playerMaxStamina;

    private bool isInMenu = false;
    public GameObject menu;

    public GameObject ingredientUI;
    public Text txtIngredientName;

    void Awake()
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
        else if(playerState == PlayerState.Skill1)
        {
            GetComponent<PlayerMove>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;

            playerAttack.weapon.SetActive(false);
            playerAttack.backWeapon.SetActive(true);
        }
        else if (playerState == PlayerState.Skill2)
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
