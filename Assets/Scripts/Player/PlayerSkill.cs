using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private Player player = null;

    private GameObject palmVortex;
    private GameObject gustSurge;

    private bool isActivePalmVortex = true;
    private bool isActiveGustSurge = true;
    private bool isFullEnergy = false;
    public bool IsActivePalmVortex { get; }
    public bool IsActiveGustSurge { get; }
    public bool IsFullEnergy { get; }

    private Vector3 scaleChange;
    private float scaleChangeSpeed = 7.0f;
    static GameObject obj = null;
    static GameObject obj2 = null;

    private float maxEnergyGauge = 100.0f;
    private float energyGauge = 0.0f;
    public bool EnergyGauge { get; }

    private float palmVortexCooltime = 5.0f;
    private float gustSurgeCooltime = 15.0f;
    public float GetPalmVortexCooltime() { return palmVortexCooltime; }
    public float GetGustSurgeCooltime() { return gustSurgeCooltime; }

    void Start()
    {
        player = GetComponent<Player>();
        palmVortex = GameObject.Find("PalmVortex");
        gustSurge = GameObject.Find("GustSurge");
        scaleChange = new Vector3(0.1f, 0.1f, 0.1f);
    }

    void Update()
    {
        if(isActivePalmVortex)
        {
            PalmVortex();
        }
        else
        {
            palmVortexCooltime -= Time.deltaTime;
            if(palmVortexCooltime <= 0.0f)
            {
                isActivePalmVortex = true;
                palmVortexCooltime = 5.0f;
            }
        }

        if (isActiveGustSurge)
        {
            if(isFullEnergy)
                GustSurge();
        }
        else
        {
            gustSurgeCooltime -= Time.deltaTime;
            if (gustSurgeCooltime <= 0.0f)
            {
                isActiveGustSurge = true;
                gustSurgeCooltime = 15.0f;
            }
        }
        UpdateGlobal();
    }

    void PalmVortex()
    {
        if (Input.GetKeyDown(KeyCode.E) && obj == null)
        {
            obj = Instantiate(palmVortex, transform.position + transform.forward * 1.3f + transform.up * 0.9f, transform.rotation);
            energyGauge += 30.0f;
            if (energyGauge > maxEnergyGauge)
                isFullEnergy = true;
        }
        if (Input.GetKey(KeyCode.E))
        {
            player.playerState = PlayerState.Elemental_Skill;
            if (obj.gameObject.transform.localScale.x <= 2.5f)
                obj.gameObject.transform.localScale += scaleChange * scaleChangeSpeed * Time.deltaTime;

            Invoke("InitPalmVortex", 3f);
        }
        else
            InitPalmVortex();
    }

    void GustSurge()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            player.playerState = PlayerState.Elemental_Burst;
            obj2 = Instantiate(gustSurge, transform.position + transform.forward * 1.3f + transform.up * 0.9f, transform.rotation);
            isActiveGustSurge = false;
            isFullEnergy = false;
            energyGauge = 0.0f;
            Invoke("InitGustSurge", 6f);
        }
    }

    void InitPalmVortex()
    {
        if (obj != null)
        {
            obj.SetActive(false);
            Destroy(obj.gameObject);
            obj = null;
            player.playerState = PlayerState.None;
            isActivePalmVortex = false;
        }
    }

    void InitGustSurge()
    {
        if (obj2 != null)
        {
            obj2.SetActive(false);
            Destroy(obj2.gameObject);
            obj2 = null;
            player.playerState = PlayerState.None;
        }
    }

    void UpdateGlobal()
    {
        UIManager.Instance.ElementalSkillCooltime = palmVortexCooltime;
        UIManager.Instance.ElementalBurstCooltime = gustSurgeCooltime;
        UIManager.Instance.IsElementalSkillCooltime = isActivePalmVortex;
        UIManager.Instance.IsElementalBurstCooltime = isActiveGustSurge;
        UIManager.Instance.IsFullEnergy = isFullEnergy;
        UIManager.Instance.EnergyGauge = energyGauge;
    }
}
