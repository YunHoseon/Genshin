using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    None = 0,
    Normal_Attack,
    Elemental_Skill,
    Elemental_Burst,
    Running,
    Climbing,
    Swimming,
    Die
}

public class Player : MonoBehaviour
{
    public PlayerState playerState;
    private Element playerElement;
    public Inventory playerInventory;
    private PlayerAttack playerAttack;
    public PlayerSkill playerSkill;

    public int playerLevel { get; set; } = 1;
    public float playerHp { get; set; }
    public float playerMaxHp = 912;
    private float playerAtk;
    private float playerGrd;
    public float PlayerStamina { get; set; }
    public float PlayerMaxStamina { get; set; } = 100.0f;

    public bool isInMenu { get; set; } = false;
    public bool isInInventory { get; set; } = false;

    public GameObject ingredientUI;
    public Text txtIngredientName;

    void Awake()
    {
        playerHp = playerMaxHp;
        playerState = PlayerState.None;
        playerAttack = GetComponent<PlayerAttack>();
        playerSkill = GetComponent<PlayerSkill>();
    }

    void Update()
    {
        OnOffPlayerScript();

        if(playerState != PlayerState.Running)
        {
            if(PlayerStamina < PlayerMaxStamina)
            {
                PlayerStamina += 10.0f * Time.deltaTime;
                if(PlayerStamina > PlayerMaxStamina)
                    PlayerStamina = PlayerMaxStamina;
            }
        }
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
            GetComponent<PlayerSkill>().enabled = true;
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
        else if (isInMenu)
        {
            //GetComponent<PlayerAnimController>().enabled = false;
            GetComponent<PlayerMove>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<PlayerSkill>().enabled = false;
        }
    }

    public void Damaged(float atk)
    {
        playerHp -= atk;
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
