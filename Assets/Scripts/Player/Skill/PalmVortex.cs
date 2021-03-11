using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PalmVortex : MonoBehaviour
{
    private List<Monster> monsters = new List<Monster>();

    public GameObject DamageText;
    public Transform ParentDamageText;

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
            GameObject damageText = Instantiate(DamageText,
                   monster.transform.position + new Vector3(Random.Range(-1.0f, 1.0f), 1, 0), Quaternion.identity);
            damageText.transform.parent = ParentDamageText;
            damageText.GetComponent<Text>().text = _damage.ToString("N0");

            monster.MonsterHp -= _damage;
            if (monster.MonsterHp < 0)
            {
                monster.Die();
                break;
            }
            
            //Debug.Log(monster.MonsterHp);
            yield return new WaitForSeconds(_time);
        }
    }

    void FloatingDamage(Monster monster, float _damage)
    {

    }
}
