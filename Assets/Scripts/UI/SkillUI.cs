using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public Text txtCooltime;
    public Image imgGauge;
    public Image imgIcon;
    private Color imgIconColor;

    void Awake()
    {
        imgGauge.fillAmount = 0;
        imgIconColor = imgIcon.color;
        imgIconColor.a = 1.0f;
        imgIcon.color = imgIconColor;

        if (imgIcon.name == "PalmVortex")
            txtCooltime.text = UIManager.Instance.elementalSkillCooltime_.ToString("N1");
        else if(imgIcon.name == "GustSurge")
            txtCooltime.text = UIManager.Instance.elementalBurstCooltime_.ToString("N1");
    }
    
    void Update()
    {
        if(UIManager.Instance.elementalSkillCooltime_ <= 5.0f && imgIcon.name == "PalmVortex")
        {
            imgGauge.fillAmount = 1;
            //imgIconColor.a = 0.3f;
            //imgIcon.color = imgIconColor;

            txtCooltime.gameObject.SetActive(true);
            txtCooltime.text = UIManager.Instance.elementalSkillCooltime_.ToString("N1");
        }
        else if(UIManager.Instance.elementalBurstCooltime_ <= 15.0f && imgIcon.name == "GustSurge")
        {
            imgGauge.fillAmount = 1;
            //imgIconColor.a = 0.3f;
            //imgIcon.color = imgIconColor;

            txtCooltime.gameObject.SetActive(true);
            txtCooltime.text = UIManager.Instance.elementalBurstCooltime_.ToString("N1");
        }
        else
        {
            txtCooltime.gameObject.SetActive(false);
        }
    }
}
