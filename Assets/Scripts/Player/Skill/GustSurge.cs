using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class GustSurge : MonoBehaviour
{
    private float moveSpeed = 2.0f;
    private List<Monster> monsters = new List<Monster>();

    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        if (monsters.Count > 0)
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i] == null)
                    return;
                monsters[i].transform.position = this.transform.position + monsters[i].offset;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            Monster monster = col.gameObject.GetComponent<Monster>();
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i] == monster)
                    return;
            }
            //Debug.Log(col.name);
            monsters.Add(monster);
            monster.offset = this.transform.position - monster.transform.position;
            StartCoroutine(TickDamage(monster, 0.8f, 7.0f));
        }
    }

    IEnumerator TickDamage(Monster monster, float _time, float _damage)
    {
        while(this.isActiveAndEnabled == true)
        {
            monster.MonsterHp -= _damage;
            if (monster.MonsterHp < 0)
                break;
            //Debug.Log(monster.MonsterHp);
            yield return new WaitForSeconds(_time);
        }
    }
}
