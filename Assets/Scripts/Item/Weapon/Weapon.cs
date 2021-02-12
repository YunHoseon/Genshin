using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Sword = 0,
    Polearm,
    Claymore,
    Catalyst,
    Bow
}

public class Weapon : Item
{
    private WeaponType weaponType;
    private float atk = 0;

    public WeaponType GetWeaponType() { return weaponType; }
    public void SetWeaponType(WeaponType type) { weaponType = type; }

    public float GetAtk() { return atk; }
    public void SetAtk(float atk_) { atk = atk_; }
}
