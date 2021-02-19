using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarUI : MonoBehaviour
{
    public Slider staminaBar;

    void Update()
    {
        if(staminaBar.value >= 1.0f)
            staminaBar.gameObject.SetActive(false);
        else
            staminaBar.gameObject.SetActive(true);

        if(staminaBar.gameObject.active)
        {
            OnOffFillArea();
        }

        float ratio = GameManager.Instance.playerStamina / 100.0f;
        staminaBar.value = ratio;
    }

    void OnOffFillArea()
    {
        if (staminaBar.value <= 0)
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        else
            transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    }
}
