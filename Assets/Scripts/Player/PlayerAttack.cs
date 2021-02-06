using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject weapon;
    public GameObject backWeapon;

    void Start()
    {

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
}
