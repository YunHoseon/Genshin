using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Dictionary<string, Monster> damagedMonsters = new Dictionary<string, Monster>();

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Monster>() == null)
            return;

        if (damagedMonsters.ContainsKey(col.name))
            return;
        else
        {
            //Debug.Log(col.name + " 추가");
            damagedMonsters.Add(col.name, col.gameObject.GetComponent<Monster>());
            col.gameObject.GetComponent<Monster>().DamagedFromSword();
        }
    }

    public void ExitSlash()
    {
        //Debug.Log("비움");
        damagedMonsters.Clear();
    }
}
