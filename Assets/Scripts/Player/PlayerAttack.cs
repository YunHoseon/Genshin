using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject weapon;
    public GameObject backWeapon;

    private PlayerAnimController playerAnimController = null;

    void Start()
    {
        playerAnimController = GetComponent<PlayerAnimController>();
    }
    
    void Update()
    {
        if(playerAnimController.isAttacking)
        {
            weapon.SetActive(true);
            backWeapon.SetActive(false);
        }
        else
        {
            weapon.SetActive(false);
            backWeapon.SetActive(true);
        }
    }
}
