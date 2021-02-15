using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject weapon;
    public GameObject backWeapon;

    void Start()
    {
        StartCoroutine(test());
    }
    
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Enemy"))
        {

        }
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("one");
        yield return new WaitForSeconds(2f);
        Debug.Log("two");
    }
}
