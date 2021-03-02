using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public float ElementalSkillCooltime { get; set; }
    public float ElementalBurstCooltime { get; set; }

    public bool IsElementalSkillCooltime { get; set; }
    public bool IsElementalBurstCooltime { get; set; }

    public bool IsFullEnergy { get; set; }
    public float EnergyGauge { get; set; }

    public GameObject menuUI;
    public GameObject inventoryWholeUI;
    public GameObject[] inventoryUI = new GameObject[4];
    public GameObject[] selectInventoryUI = new GameObject[4];
    public Text txtItemKind;

    public GameObject HpBar;
    public GameObject StaminaBar;
    public GameObject SkillUI;

    public int inventoryNum = 0;

    void Start()
    {
        for(int i = 0; i < inventoryUI.Length; i++)
            inventoryUI[i].SetActive(false);
        for (int i = 0; i < selectInventoryUI.Length; i++)
            selectInventoryUI[i].SetActive(false);
        inventoryWholeUI.SetActive(false);
    }

    public void StartGame()
    {
        GameManager.Instance.SetIsStart(true);
    }

    public void GoToStartScene()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.SetIsExitGame(true);
    }

    void Update()
    {
        GoToMenu();

        if (GameManager.Instance.Player.isInMenu || GameManager.Instance.Player.isInInventory)
        {
            HpBar.SetActive(false);
            StaminaBar.SetActive(false);
            SkillUI.SetActive(false);
        }
        else
        {
            HpBar.SetActive(true);
            StaminaBar.SetActive(true);
            SkillUI.SetActive(true);
        }
    }

    void GoToMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameManager.Instance.Player.isInInventory)
            {
                GameManager.Instance.Player.isInInventory = false;
                inventoryUI[inventoryNum].SetActive(false);
                inventoryWholeUI.SetActive(false);
            }
            GameManager.Instance.Player.isInMenu = GameManager.Instance.Player.isInMenu ? false : true;
            menuUI.SetActive(GameManager.Instance.Player.isInMenu);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            GoToInventory();
        }
    }

    public void GoToInventory()
    {
        if(GameManager.Instance.Player.isInMenu)
        {
            GameManager.Instance.Player.isInMenu = false;
            menuUI.SetActive(false);
        }
        GameManager.Instance.Player.isInInventory = GameManager.Instance.Player.isInInventory ? false : true;
        inventoryUI[inventoryNum].SetActive(GameManager.Instance.Player.isInInventory);
        selectInventoryUI[inventoryNum].SetActive(true);
        inventoryWholeUI.SetActive(GameManager.Instance.Player.isInInventory);
    }

    public void ClickWeaponInventory()
    {
        inventoryNum = 0;
        inventoryUI[0].SetActive(true);
        inventoryUI[1].SetActive(false);
        inventoryUI[2].SetActive(false);
        inventoryUI[3].SetActive(false);

        selectInventoryUI[0].SetActive(true);
        selectInventoryUI[1].SetActive(false);
        selectInventoryUI[2].SetActive(false);
        selectInventoryUI[3].SetActive(false);
        txtItemKind.text = "장비 아이템";
    }

    public void ClickArtifactInventory()
    {
        inventoryNum = 1;
        inventoryUI[0].SetActive(false);
        inventoryUI[1].SetActive(true);
        inventoryUI[2].SetActive(false);
        inventoryUI[3].SetActive(false);

        selectInventoryUI[0].SetActive(false);
        selectInventoryUI[1].SetActive(true);
        selectInventoryUI[2].SetActive(false);
        selectInventoryUI[3].SetActive(false);
        txtItemKind.text = "성유물 아이템";
    }

    public void ClickIngredientInventory()
    {
        inventoryNum = 2;
        inventoryUI[0].SetActive(false);
        inventoryUI[1].SetActive(false);
        inventoryUI[2].SetActive(true);
        inventoryUI[3].SetActive(false);

        selectInventoryUI[0].SetActive(false);
        selectInventoryUI[1].SetActive(false);
        selectInventoryUI[2].SetActive(true);
        selectInventoryUI[3].SetActive(false);
        txtItemKind.text = "재료 아이템";
    }

    public void ClickNurtureInventory()
    {
        inventoryNum = 3;
        inventoryUI[0].SetActive(false);
        inventoryUI[1].SetActive(false);
        inventoryUI[2].SetActive(false);
        inventoryUI[3].SetActive(true);

        selectInventoryUI[0].SetActive(false);
        selectInventoryUI[1].SetActive(false);
        selectInventoryUI[2].SetActive(false);
        selectInventoryUI[3].SetActive(true);
        txtItemKind.text = "육성 아이템";
    }

    public void QuitInventory()
    {
        GameManager.Instance.Player.isInInventory = false;
        inventoryUI[inventoryNum].SetActive(false);
        inventoryWholeUI.SetActive(false);
    }
}
