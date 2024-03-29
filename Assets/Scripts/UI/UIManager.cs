﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public int monsterCount = 0;
    private float timer = 0;
    private bool isClearQuest0 = false;
    private bool isClearQuest1 = false;

    /* inventory */

    public GameObject menuUI;
    public GameObject inventoryWholeUI;
    public GameObject[] inventoryUI = new GameObject[4];
    public GameObject[] selectInventoryUI = new GameObject[4];
    public Text txtItemKind;

    public int inventoryNum = 0;

    /* Character */
    public GameObject Map;
    public GameObject GameObjects;
    public GameObject InGameUI;
    public GameObject MenuObjects;

    public GameObject CharacterUI;

    /* Tutorial */
    public GameObject[] TutorialUI = new GameObject[4];

    /* Quest */
    public Text txtTitle;
    public Text txtContent;
    public Text txtClear;

    private SoundManager soundManager;

    public GameObject settingUI;
    public Slider BGMSlider;
    public Slider FXSlider;

    void Start()
    {
        soundManager = SoundManager.instance;

        for (int i = 0; i < inventoryUI.Length; i++)
            inventoryUI[i].SetActive(false);
        for (int i = 0; i < selectInventoryUI.Length; i++)
            selectInventoryUI[i].SetActive(false);
        inventoryWholeUI.SetActive(false);
    }

    public void GoToStartScene()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.SetIsExitGame(true);
    }

    void Update()
    {
        GoToMenu();

        if (GameManager.Instance.Player.isInMenu || GameManager.Instance.Player.isInInventory ||
            GameManager.Instance.Player.isInCharacter)
        {
            InGameUI.SetActive(false);
        }
        else
        {
            InGameUI.SetActive(true);
        }
        
        SetActiveTutorialUI();

        if(isClearQuest0 && !isClearQuest1)
            txtContent.text = "몬스터를 모두 무찌르자 ("+ monsterCount +"/3)";

        if (monsterCount >= 3 && !isClearQuest1)
            MonsterQuestClear();
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
            else if(GameManager.Instance.Player.isInCharacter)
            {
                GameManager.Instance.Player.isInCharacter = false;
                CharacterUI.SetActive(false);

                Map.SetActive(true);
                GameObjects.SetActive(true);
                InGameUI.SetActive(true);
                MenuObjects.SetActive(true);
            }
            GameManager.Instance.Player.isInMenu = GameManager.Instance.Player.isInMenu ? false : true;
            menuUI.SetActive(GameManager.Instance.Player.isInMenu);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            GoToInventory();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            if(!GameManager.Instance.Player.isInCharacter)
                GoToCharacter();
            else
            {
                QuitCharacter();
            }
        }
    }

    public void GoToInventory()
    {
        if (GameManager.Instance.Player.isInCharacter)
            return;

        if (GameManager.Instance.Player.isInMenu)
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

    public void GoToCharacter()
    {
        if (GameManager.Instance.Player.isInInventory)
            return;

        menuUI.SetActive(false);
        GameManager.Instance.Player.isInCharacter = GameManager.Instance.Player.isInCharacter ? false : true;
        CharacterUI.SetActive(GameManager.Instance.Player.isInCharacter);

        Map.SetActive(false);
        GameObjects.SetActive(false);
        InGameUI.SetActive(false);
        MenuObjects.SetActive(false);
    }

    public void GoToSetting()
    {
        settingUI.SetActive(true);
        menuUI.SetActive(false);
    }

   public void ChangeValueSlider()
    {
        SoundManager.instance.GameBGM.volume = BGMSlider.value;

        SoundManager.instance.AttackAudio.volume = FXSlider.value;
        SoundManager.instance.DamagedAudio.volume = FXSlider.value;
        SoundManager.instance.OKAudio.volume = FXSlider.value;
        SoundManager.instance.QuitAudio.volume = FXSlider.value;
        SoundManager.instance.SwitchAudio.volume = FXSlider.value;
    }

    public void QuitMenu()
    {
        GameManager.Instance.Player.isInMenu = false;
        menuUI.SetActive(false);
    }

    public void QuitInventory()
    {
        GameManager.Instance.Player.isInInventory = false;
        inventoryUI[inventoryNum].SetActive(false);
        inventoryWholeUI.SetActive(false);
    }

    public void QuitCharacter()
    {
        GameManager.Instance.Player.isInCharacter = false;
        CharacterUI.SetActive(false);

        Map.SetActive(true);
        GameObjects.SetActive(true);
        InGameUI.SetActive(true);
        MenuObjects.SetActive(true);

        if(GameManager.Instance.Player.isInMenu)
            menuUI.SetActive(true);
        else
            menuUI.SetActive(false);
    }

    public void QuitSetting()
    {
        settingUI.SetActive(false);
        menuUI.SetActive(true);
    }

    private void SetActiveTutorialUI()
    {
        if(GameManager.Instance.Player.isClearedControlTutorial[3] == 2)
        {
            TutorialUI[3].SetActive(false);
            return;
        }

        for(int i = 0; i < TutorialUI.Length; i++)
        {
            if (GameManager.Instance.Player.isClearedControlTutorial[i] == 1)
                TutorialUI[i].SetActive(true);
            else
                TutorialUI[i].SetActive(false);
        }
    }

    public void OnQuestClear()
    {
        txtClear.gameObject.SetActive(true);
        isClearQuest0 = true;

        Invoke("ToNextQuest", 3.0f);
    }

    void ToNextQuest()
    {
        if(isClearQuest0 && !isClearQuest1)
        {
            txtTitle.text = "전투 배우기";
            txtContent.text = "몬스터를 모두 무찌르자 (0/3)";
            txtClear.gameObject.SetActive(false);
        }
        else
        {
            txtTitle.gameObject.SetActive(false);
            txtContent.gameObject.SetActive(false);
            txtClear.gameObject.SetActive(false);
        }
    }

    public void MonsterQuestClear()
    {
        txtClear.gameObject.SetActive(true);
        isClearQuest1 = true;

        Invoke("ToNextQuest", 3.0f);
    }

    public void ToLoadingScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
