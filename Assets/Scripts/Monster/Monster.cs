using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    Idle = 0,
    Moving,
    Attaking,
    Damaged,
    Die,
    InMenu
}

public class Monster : MonoBehaviour
{
    public MonsterState monsterState;
    private Element monsterElement;
    private Animator animator;

    public GameObject player;

    private float monsterMaxHp;
    private float monsterHp;
    private float monsterAtk;
    private float monsterGrd;

    private bool isInMenu = false;

    void Awake()
    {
        monsterState = MonsterState.Idle;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if(monsterHp <= 0)
        {
            //Destroy(this.gameObject, 0.1f);
        }

        if(Vector3.Distance(player.transform.position, transform.position) < 2.0f)
        {
            monsterState = MonsterState.Attaking;
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }

    void OntriggerEnter(Collider col)
    {
        if(col.CompareTag("Weapon"))
        {

        }
        if (col.CompareTag("PalmVortex"))
        {

        }
        if (col.CompareTag("GustSurge"))
        {

        }
    }
}
