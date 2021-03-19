using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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

    public int[] isClearedControlTutorial = new int[4];
    private float currDistance = 5.0f;
    private float h, v;

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

    public Transform ParentDamageText;
    public GameObject DamageText;

    [SerializeField]
    private InventoryUI[] playerInventory = new InventoryUI[4];

    public UnityEvent OnQuestClear;

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

        isClearedControlTutorial[0] = 1;
        isClearedControlTutorial[1] = 0;
        isClearedControlTutorial[2] = 0;
        isClearedControlTutorial[3] = 0;
    }

    void Update()
    {
        //Debug.Log("playerExp : " + playerExp);
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
        
        if(isClearedControlTutorial[3] != 2)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            InputPlayer();
        }
    }

    void InputPlayer()
    {
        currDistance -= Input.GetAxis("Mouse ScrollWheel") * 10;

        if (currDistance != 5 && isClearedControlTutorial[0] == 1)
        {
            isClearedControlTutorial[0] = 0;
            isClearedControlTutorial[1] = 1;
        }
        else if((h != 0 || v != 0) && isClearedControlTutorial[1] == 1)
        {
            isClearedControlTutorial[1] = 0;
            isClearedControlTutorial[2] = 1;
        }
        else if (Input.GetMouseButtonDown(1) && isClearedControlTutorial[2] == 1)
        {
            isClearedControlTutorial[2] = 0;
            isClearedControlTutorial[3] = 1;
        }
        else if(Input.GetButtonDown("Jump") && isClearedControlTutorial[3] == 1)
        {
            isClearedControlTutorial[3] = 2;
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
        GameObject damageText = Instantiate(DamageText,
                    transform.position + new Vector3(Random.Range(-1.0f, 1.0f), 1, 0), Quaternion.identity);
        damageText.transform.localScale = new Vector3(2, 2, 2);
        Debug.Log(damageText.transform.localScale);
        damageText.transform.parent = ParentDamageText;
        damageText.GetComponent<Text>().text = ((int)(atk * (100 / (100 + playerGrd)))).ToString("N0");

        playerHp -= (int)(atk  * (100 / (100 + playerGrd)));
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Quest"))
        {
            Destroy(col.gameObject);
            OnQuestClear.Invoke();
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Ingredient"))
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
