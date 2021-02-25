using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject inventoryUI;

    public GameObject HpBar;
    public GameObject StaminaBar;
    public GameObject SkillUI;

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

        if (GameManager.Instance.player.isInMenu)
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
            if(GameManager.Instance.player.isInInventory)
            {
                GameManager.Instance.player.isInInventory = false;
                inventoryUI.SetActive(false);
            }
            GameManager.Instance.player.isInMenu = GameManager.Instance.player.isInMenu ? false : true;
            menuUI.SetActive(GameManager.Instance.player.isInMenu);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            GoToInventory();
        }
    }

    public void GoToInventory()
    {
        if(GameManager.Instance.player.isInMenu)
        {
            GameManager.Instance.player.isInMenu = false;
            menuUI.SetActive(false);
        }
        GameManager.Instance.player.isInInventory = GameManager.Instance.player.isInInventory ? false : true;
        inventoryUI.SetActive(GameManager.Instance.player.isInInventory);
    }
}
