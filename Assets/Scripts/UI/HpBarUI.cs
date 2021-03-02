using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    private Slider hpBar;
    public Text txtHp;
    public Text txtLevel;

    void Start()
    {
        hpBar = GetComponentInChildren<Slider>();
    }
    
    void Update()
    {
        OnOffFillArea();

        txtHp.text = GameManager.Instance.Player.playerHp.ToString("N0") + " / " + GameManager.Instance.Player.playerMaxHp.ToString("N0");
        txtLevel.text = "Lv." + GameManager.Instance.Player.playerLevel.ToString("N0");

        float ratio = GameManager.Instance.Player.playerHp / GameManager.Instance.Player.playerMaxHp;
        hpBar.value = ratio;
    }

    void OnOffFillArea()
    {
        if (hpBar.value <= 0)
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        else
            transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    }
}
