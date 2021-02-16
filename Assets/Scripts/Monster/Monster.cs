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

    private float monsterHp;
    private float monsterAtk;
    private float monsterGrd;

    private bool isInMenu = false;

    void Awake()
    {
        monsterState = MonsterState.None;
    }

    void OntriggerEnter(Collider col)
    {
        if(col.CompareTag("Weapon"))
        {

        }
    }
}
