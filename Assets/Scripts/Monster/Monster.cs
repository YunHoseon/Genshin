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

    public Transform ParentDamageText;
    public GameObject DamageText;

    private Vector3 originPos;

    public GameObject SkinnedMesh;
    public Material DissolveMat;

    private float moveSpeed = 1.0f;
    private float findDistance = 10.0f;
    private float attackDistance = 3.0f;
    //private float moveDistance = 10.0f;

    private int monsterLevel = 1;
    private float monsterMaxHp = 50.0f;
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
    private float monsterAtk = 20.0f;
    private float monsterGrd = 20.0f;

    private bool isInMenu = false;
    private bool isAttacked = false;
    public Vector3 offset;      // for gustSurge

    [SerializeField]
    private ParticleSystem damagedEffect;

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
                break;
            case MonsterState.Return:
                Return();
                break;
            case MonsterState.Die:
                break;
        }
        //Debug.Log("dist : " + (int)Vector3.Distance(player.transform.position, transform.position));
    }

    IEnumerator CheckDist()
    {
        Debug.Log("dist : " + Vector3.Distance(player.transform.position, transform.position));
        yield return new WaitForSeconds(1.0f);
    }

    void Idle()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= findDistance)
        {
            animator.SetBool("Move", true);
            monsterState = MonsterState.Moving;
        }
    }

    void Moving()
    {
        if (Vector3.Distance(transform.position, originPos) > findDistance ||
            Vector3.Distance(player.transform.position, transform.position) > findDistance)
        {
            monsterState = MonsterState.Return;
        }

        if (Vector3.Distance(player.transform.position, transform.position) > attackDistance &&
            Vector3.Distance(player.transform.position, transform.position) < findDistance)
        {
            //Vector3 dir = (player.transform.position - transform.position).normalized;
            //this.gameObject.GetComponent<Rigidbody>().MovePosition(dir * moveSpeed * Time.deltaTime);
            this.transform.LookAt(player.transform);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else if(Vector3.Distance(player.transform.position, transform.position) <= attackDistance)
        {
            animator.SetBool("Move", false);
            animator.SetBool("Attack", true);
            monsterState = MonsterState.Attaking;
        }
    }

    void Attack()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= attackDistance)
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
        StartCoroutine(DamageProcess());
        //UIManager.Instance.PlayDamagedSound();
    }

    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(0.6f);

        monsterState = MonsterState.Moving;
    }

    void Return()
    {
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            this.transform.LookAt(originPos);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = originPos;
            animator.SetBool("Move", false);
            monsterState = MonsterState.Idle;
        }
    }

    public void Die()
    {
        if(this.gameObject != null)
        {
            GameManager.Instance.Player.PlayerExp += 50;
            UIManager.Instance.monsterCount += 1;
            animator.SetBool("Die", true);
            SkinnedMesh.GetComponent<SkinnedMeshRenderer>().material = DissolveMat;

            Destroy(this.gameObject, 2f);
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
        if (col.CompareTag("Weapon"))
        {
            if (monsterHp > 0)
            {
                GameObject damageText = Instantiate(DamageText,
                    transform.position + new Vector3(Random.Range(-1.0f, 1.0f), 1, 0), Quaternion.identity);
                damageText.transform.parent = ParentDamageText;
                damageText.GetComponent<Text>().text = (GameManager.Instance.Player.PlayerAtk * (100 / (100 + monsterGrd))).ToString("N0");
                damagedEffect.Play();
                SoundManager.instance.PlayDamagedSound();

                animator.SetTrigger("Damaged");
                monsterState = MonsterState.Damaged;
                monsterHp -= GameManager.Instance.Player.PlayerAtk * (100 / (100 + monsterGrd));
                Damaged();
            }
            else
            {
                monsterState = MonsterState.Die;
                Die();
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("PalmVortex") || col.CompareTag("GustSurge"))
        {
            if (monsterHp > 0)
            {
                animator.SetTrigger("Damaged");
                monsterState = MonsterState.Damaged;
                Damaged();
            }
        }
    }
}
