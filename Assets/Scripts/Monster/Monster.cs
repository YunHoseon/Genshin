using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    None = 0,
    Attaking,
    Moving,
    Die,
    InMenu
}

public class Monster : MonoBehaviour
{
    public MonsterState monsterState;
    private Element monsterElement;

    private float monsterMaxHp;
    private float monsterHp;
    private float monsterAtk;
    private float monsterGrd;

    private bool isInMenu = false;

    void Awake()
    {
        monsterState = MonsterState.None;
    }

    void Update()
    {
        if(monsterHp <= 0)
        {
            Destroy(this.gameObject, 0.1f);
        }
    }

    void OntriggerEnter(Collider col)
    {
        if(col.CompareTag("Weapon"))
        {

        }
    }
}
