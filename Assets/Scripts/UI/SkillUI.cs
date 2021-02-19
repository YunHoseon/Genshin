using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public Text txtCooltime;
    public Image imgGauge;
    public Image imgIcon;
    public Image imgEnergyGauge;
    private Color imgIconColor;

    void Awake()
    {
        imgGauge.fillAmount = 0;
        imgIconColor = imgIcon.color;
        imgIconColor.a = 1.0f;
        imgIcon.color = imgIconColor;

        if (imgIcon.name == "PalmVortex")
            txtCooltime.text = UIManager.Instance.ElementalSkillCooltime.ToString("N1");
        else if(imgIcon.name == "GustSurge")
            txtCooltime.text = UIManager.Instance.ElementalBurstCooltime.ToString("N1");
    }
    
    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (UIManager.Instance.IsElementalSkillCooltime && imgIcon.name == "PalmVortex")
        {
            txtCooltime.gameObject.SetActive(false);
            imgIconColor.a = 1.0f;
            imgIcon.color = imgIconColor;
        }
        else if (imgIcon.name == "PalmVortex")
        {
            imgGauge.fillAmount = 1;
            imgIconColor.a = 0.3f;
            imgIcon.color = imgIconColor;

            float ratio = 1.0f - (UIManager.Instance.ElementalSkillCooltime / 5.0f);
            imgGauge.fillAmount = ratio;

            txtCooltime.gameObject.SetActive(true);
            txtCooltime.text = UIManager.Instance.ElementalSkillCooltime.ToString("N1");
        }

        if (UIManager.Instance.IsElementalBurstCooltime && UIManager.Instance.IsFullEnergy && imgIcon.name == "GustSurge")
        {
            txtCooltime.gameObject.SetActive(false);
            imgIconColor.a = 1.0f;
            imgIcon.color = imgIconColor;

            float ratio = UIManager.Instance.EnergyGauge / 100.0f;
            if (ratio > 1.0f)
                ratio = 1.0f;
            imgEnergyGauge.fillAmount = ratio;
        }
        else if(!UIManager.Instance.IsFullEnergy && imgIcon.name == "GustSurge")
        {
            txtCooltime.gameObject.SetActive(false);
            imgIconColor.a = 0.3f;
            imgIcon.color = imgIconColor;

            float ratio = UIManager.Instance.EnergyGauge / 100.0f;
            if (ratio > 1.0f)
                ratio = 1.0f;
            imgEnergyGauge.fillAmount = ratio;
        }

        if(!UIManager.Instance.IsFullEnergy && !UIManager.Instance.IsElementalBurstCooltime && imgIcon.name == "GustSurge")
        {
            imgGauge.fillAmount = 1;
            imgIconColor.a = 0.3f;
            imgIcon.color = imgIconColor;

            float ratio = 1.0f - (UIManager.Instance.ElementalBurstCooltime / 15.0f);
            imgGauge.fillAmount = ratio;

            txtCooltime.gameObject.SetActive(true);
            txtCooltime.text = UIManager.Instance.ElementalBurstCooltime.ToString("N1");

            float ratio_ = UIManager.Instance.EnergyGauge / 100.0f;
            if (ratio_ > 1.0f)
                ratio_ = 1.0f;
            imgEnergyGauge.fillAmount = ratio_;
        }
    }
}
