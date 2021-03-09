using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public Text txtPlayerLevel;
    public Text txtPlayerExp;
    public Text txtPlayerMaxHp;
    public Text txtPlayerAtk;
    public Text txtPlayerGrd;
    public Text txtPlayerMaxStamina;

    public Slider playerExpBar;

    void Update()
    {
        UpdateData();
    }

    void UpdateData()
    {
        txtPlayerLevel.text = "Lv." + GameManager.Instance.Player.playerLevel.ToString("N0");
        txtPlayerExp.text = GameManager.Instance.Player.PlayerExp.ToString("N0") + "/" + GameManager.Instance.Player.PlayerMaxExp.ToString("N0");
        txtPlayerMaxHp.text = GameManager.Instance.Player.playerMaxHp.ToString("N0");
        txtPlayerAtk.text = GameManager.Instance.Player.PlayerAtk.ToString("N0");
        txtPlayerGrd.text = GameManager.Instance.Player.PlayerGrd.ToString("N0");
        txtPlayerMaxStamina.text = GameManager.Instance.Player.PlayerMaxStamina.ToString("N0");

        float ratio = GameManager.Instance.Player.PlayerExp / GameManager.Instance.Player.PlayerMaxExp;
        playerExpBar.value = ratio;
    }
}
