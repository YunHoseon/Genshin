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
    private PlayerAttack playerAttack;
    public PlayerSkill playerSkill;

    public int playerLevel { get; set; } = 1;
    public float playerHp { get; set; }
    public float playerMaxHp;
    private float playerAtk;
    public float PlayerAtk
    {
        get
        {
            return this.playerAtk;
        }
    }
    private float playerGrd;
    public float PlayerGrd
    {
        get
        {
            return this.playerGrd;
        }
    }
    private float playerExp = 0;
    public float PlayerExp
    {
        get
        {
            return this.playerExp;
        }
        set
        {
            playerExp = value;
        }
    }
    private float playerMaxExp;
    public float PlayerMaxExp
    {
        get
        {
            return this.playerMaxExp;
        }
    }
    public float PlayerStamina { get; set; }
    public float PlayerMaxStamina { get; set; } = 100.0f;

    public bool isInMenu = false;
    public bool isInInventory = false;
    public bool isInCharacter = false;

    public GameObject startSword;
    public GameObject ingredientUI;
    public Text txtIngredientName;

    [SerializeField]
    private InventoryUI[] playerInventory = new InventoryUI[4];

    void Awake()
    {
        SetData();
        playerHp = playerMaxHp;
        playerState = PlayerState.None;
        playerAttack = GetComponent<PlayerAttack>();
        playerSkill = GetComponent<PlayerSkill>();
    }

    void Start()
    {
        playerInventory[1].AcquireItem(startSword.GetComponent<Item>(), 1);
    }

    void Update()
    {
        Debug.Log("playerExp : " + playerExp);
        OnOffPlayerScript();
        LevelUp();

        if (playerState != PlayerState.Running)
        {
            if(PlayerStamina < PlayerMaxStamina)
            {
                PlayerStamina += 10.0f * Time.deltaTime;
                if(PlayerStamina > PlayerMaxStamina)
                    PlayerStamina = PlayerMaxStamina;
            }
        }
    }

    void SetData()
    {
        playerMaxHp = float.Parse(PlayerStatDatabase.PsInstance.playerStatusDB[playerLevel - 1]["MaxHp"].ToString());
        playerAtk = float.Parse(PlayerStatDatabase.PsInstance.playerStatusDB[playerLevel - 1]["Atk"].ToString());
        playerGrd = float.Parse(PlayerStatDatabase.PsInstance.playerStatusDB[playerLevel - 1]["Grd"].ToString());
        playerMaxExp = float.Parse(PlayerStatDatabase.PsInstance.playerStatusDB[playerLevel - 1]["MaxExp"].ToString());
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
        else if (isInMenu || isInInventory || isInCharacter)
        {
            GetComponent<PlayerMove>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<PlayerSkill>().enabled = false;
        }
        else if(!isInMenu && !isInInventory && !isInCharacter)
        {
            if(playerState == PlayerState.Normal_Attack)
                GetComponent<PlayerAttack>().enabled = true;

            GetComponent<PlayerMove>().enabled = true;
            GetComponent<PlayerSkill>().enabled = true;
        }
    }

    void LevelUp()
    {
        if(playerExp >= playerMaxExp)
        {
            playerExp = playerExp - playerMaxExp;
            playerLevel += 1;
            SetData();
            playerHp = playerMaxHp;
        }
    }

    public void Damaged(float atk)
    {
        playerHp -= atk  * (100 / 100 + playerGrd);
    }

    void OnTriggerStay(Collider col)
    {
        if(col.CompareTag("Ingredient"))
        {
            txtIngredientName.text = col.name;
            ingredientUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                ingredientUI.SetActive(false);
                playerInventory[0].AcquireItem(col.GetComponent<Item>(), 1);
                col.gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Ingredient"))
            ingredientUI.SetActive(false);
    }
}
