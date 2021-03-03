using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmVortex : MonoBehaviour
{
    private List<Monster> monsters = new List<Monster>();

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            monsters.Add(col.gameObject.GetComponent<Monster>());
        }

        if (monsters.Count > 0)
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                StartCoroutine(TickDamage(monsters[i], 1.0f, 5.0f));
            }
        }
    }

    IEnumerator TickDamage(Monster monster, float _time, float _damage)
    {
        while (this.isActiveAndEnabled == true)
        {
            monster.MonsterHp -= _damage;
            if (monster.MonsterHp < 0)
                break;
            Debug.Log(monster.MonsterHp);
            yield return new WaitForSeconds(_time);
        }
    }

    void FloatingDamage(Monster monster, float _damage)
    {

    }
}
