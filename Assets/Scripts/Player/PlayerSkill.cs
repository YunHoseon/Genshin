using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private Player player = null;

    private GameObject swordStorm;
    private GameObject tornado;
    private bool isActiveSwordStorm = true;
    private bool isActiveTornado = true;

    private Vector3 scaleChange;
    private float scalaeChangeSpeed = 7.0f;
    static GameObject obj = null;
    static GameObject obj2 = null;

    private float swordStormCoolTime = 5.0f;
    private float tornadoCoolTime = 15.0f;

    void Start()
    {
        player = GetComponent<Player>();
        swordStorm = GameObject.Find("StormSward");
        tornado = GameObject.Find("Tornado");
        scaleChange = new Vector3(0.1f, 0.1f, 0.1f);
    }

    void Update()
    {
        if(isActiveSwordStorm)
        {
            SwardStorm();
        }
        else
        {
            swordStormCoolTime -= Time.deltaTime;
            if(swordStormCoolTime <= 0.0f)
            {
                isActiveSwordStorm = true;
                swordStormCoolTime = 5.0f;
            }
        }

        if (isActiveTornado)
        {
            TornadoSkill();
        }
        else
        {
            tornadoCoolTime -= Time.deltaTime;
            if (tornadoCoolTime <= 0.0f)
            {
                isActiveTornado = true;
                tornadoCoolTime = 15.0f;
            }
        }
    }

    void SwardStorm()
    {
        if (Input.GetKeyDown(KeyCode.E) && obj == null)
        {
            obj = Instantiate(swordStorm, transform.position + transform.forward * 1.3f + transform.up * 0.9f, transform.rotation);
        }
        if (Input.GetKey(KeyCode.E))
        {
            player.playerState = PlayerState.Skill1;
            if (obj.gameObject.transform.localScale.x <= 2.5f)
                obj.gameObject.transform.localScale += scaleChange * scalaeChangeSpeed * Time.deltaTime;

            Invoke("InitSwardStorm", 3f);
        }
        else
            InitSwardStorm();
    }

    void TornadoSkill()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            player.playerState = PlayerState.Skill2;
            obj2 = Instantiate(tornado, transform.position + transform.forward * 1.3f + transform.up * 0.9f, transform.rotation);
            Invoke("InitTornado", 6f);
        }
    }

    void InitSwardStorm()
    {
        if (obj != null)
        {
            obj.SetActive(false);
            Destroy(obj.gameObject);
            obj = null;
            player.playerState = PlayerState.None;
            isActiveSwordStorm = false;
        }
    }

    void InitTornado()
    {
        if (obj2 != null)
        {
            obj2.SetActive(false);
            Destroy(obj2.gameObject);
            obj2 = null;
            player.playerState = PlayerState.None;
            isActiveTornado = false;
        }
    }
}
