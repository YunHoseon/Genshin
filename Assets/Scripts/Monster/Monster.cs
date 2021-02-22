using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Slider hpBar;
    public Text txtLevel;
    public GameObject fillArea;

    private GameObject player;
    private GameObject attackCollider;

    private float findDistance = 2.0f;

    private int monsterLevel = 1;
    private float monsterMaxHp = 30.0f;
    private float monsterHp;
    private float monsterAtk = 10.0f;
    private float monsterGrd = 5.0f;

    private bool isInMenu = false;

    void Awake()
    {
        monsterHp = monsterMaxHp;
        monsterState = MonsterState.Idle;
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        player = GameObject.Find("Player");
        attackCollider = transform.GetChild(1).gameObject;
        txtLevel.text = "Lv." + monsterLevel.ToString("N0");
    }

    void Update()
    {
        OnOffFillArea();

        float ratio = monsterHp / monsterMaxHp;
        hpBar.value = ratio;

        if (monsterHp <= 0)
        {
            monsterState = MonsterState.Die;
            Destroy(this.gameObject, 0.1f);
        }

        switch (monsterState)
        {
            case MonsterState.Idle:
                Idle();
                break;
            case MonsterState.Moving:
                break;
            case MonsterState.Attaking:
                Attack();
                break;
            case MonsterState.Damaged:
                break;
            case MonsterState.Die:
                break;
        }
    }

    void Idle()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < findDistance)
        {
            monsterState = MonsterState.Attaking;
            animator.SetBool("Attack", true);
        }
    }

    void Attack()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < findDistance)
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            monsterState = MonsterState.Idle;
            animator.SetBool("Attack", false);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Weapon"))
        {
            monsterHp -= 10;
        }
        if (col.CompareTag("PalmVortex"))
        {

        }
        if (col.CompareTag("GustSurge"))
        {

        }
    }

    public void ActiveCol()
    {
        StartCoroutine("AttackTimer");
    }

    IEnumerator AttackTimer()
    {
        attackCollider.SetActive(true);
        GameManager.Instance.player.Damaged(monsterAtk);
        yield return new WaitForSeconds(0.5f);
        
        attackCollider.SetActive(false);
    }

    void OnOffFillArea()
    {
        if (hpBar.value <= 0)
            fillArea.SetActive(false);
        else
            fillArea.SetActive(true);
    }
}
