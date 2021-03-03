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
    Return,
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

    private Vector3 originPos;

    private float moveSpeed = 2.0f;
    private float findDistance = 3.0f;
    private float attackDistance = 1.5f;
    private float moveDistance = 10.0f;

    private int monsterLevel = 1;
    private float monsterMaxHp = 30.0f;
    private float monsterHp;
    public float MonsterHp
    {
        get
        {
            return this.monsterHp;
        }
        set
        {
            this.monsterHp = value;
        }
    }
    private float monsterAtk = 10.0f;
    private float monsterGrd = 5.0f;

    private bool isInMenu = false;
    private bool isAttacked = false;
    public Vector3 offset;      // for gustSurge
    
    void Awake()
    {
        monsterHp = monsterMaxHp;
        monsterState = MonsterState.Idle;
        animator = GetComponentInChildren<Animator>();
        originPos = this.transform.position;
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
                Moving();
                break;
            case MonsterState.Attaking:
                Attack();
                break;
            case MonsterState.Damaged:
                Damaged();
                break;
            case MonsterState.Return:
                Return();
                break;
            case MonsterState.Die:
                break;
        }
    }

    void Idle()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < findDistance)
        {
            animator.SetBool("Move", true);
            monsterState = MonsterState.Moving;
        }
    }

    void Moving()
    {
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            monsterState = MonsterState.Return;
        }

        if (Vector3.Distance(transform.position, originPos) > attackDistance)
        {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            this.gameObject.GetComponent<Rigidbody>().MovePosition(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Move", false);
            //animator.SetBool("Attack", true);
            monsterState = MonsterState.Attaking;
        }
    }

    void Attack()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < attackDistance)
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
            animator.SetBool("Move", true);
            monsterState = MonsterState.Moving;
        }
    }

    void Damaged()
    {

    }

    void Return()
    {
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            this.gameObject.GetComponent<Rigidbody>().MovePosition(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = originPos;
            monsterState = MonsterState.Idle;
        }
    }

    public void ActiveCol()
    {
        StartCoroutine("AttackTimer");
    }

    IEnumerator AttackTimer()
    {
        attackCollider.SetActive(true);
        GameManager.Instance.Player.Damaged(monsterAtk);
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

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Weapon") && !isAttacked)
        {
            monsterHp -= 10;
            isAttacked = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Weapon"))
            isAttacked = false;
    }
}
